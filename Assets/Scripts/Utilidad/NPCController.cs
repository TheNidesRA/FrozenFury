using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using GridSystem;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace UtilityBehaviour
{
    public class NPCController : MonoBehaviour
    {
        public MoveController mover { get; set; }
        public AIBrain aiBrain { get; set; }
        public UtilityAction[] actionsAviable;
        private bool doingAction = false;
        private System.Action callback;
        public const float MAXFATIGUE = 100;
        public const float MAXTOOLDURABILITY = 1000;

        public Transform HousePosition;

        public Image actionImage;
        public Button PayButton;
        public TextMeshProUGUI DineroAPagar;
        public int dineroNecesarioAPagar = 0;
        public GameObject PanelDePago;

        public Transform[] WayPointsPatrol;
        private int waypoint;

        public string accionActual;
        public float scoreAccionActual;
        public float sleepTime = 0;

        [SerializeField] private float _timeWorked;

        [SerializeField] private float _toolDurability;

        public Animator characterAnimator;

        public CherryEnergyBar EnergyBar;

        private void Awake()
        {
            TimeWorked = MAXFATIGUE;
            ToolDurability = MAXTOOLDURABILITY;
            waypoint = 0;
            PayButton.enabled = false;
            dineroNecesarioAPagar = 0;

            characterAnimator = GetComponent<Animator>();
        }


        private void OnEnable()
        {
            WaveController._instance.OnRoundActive += On_RoundActive;
            actionImage.gameObject.SetActive(true);
            // Aparecer();
        }

        private void OnDisable()
        {
            WaveController._instance.OnRoundActive -= On_RoundActive;
            StopAllCoroutines();
        }

        private void On_RoundActive(object sender, bool e)
        {
            if (e)
            {
                mover.Resetear();
                actionImage.gameObject.SetActive(false);
                StartCoroutine(Desaparecer());

                // gameObject.SetActive(false);
            }
            else
            {
                actionImage.gameObject.SetActive(true);
                aiBrain.DecideBestAction(actionsAviable);
            }
        }

        public event EventHandler<float> OnTimeWorkedChanged;

        public float TimeWorked
        {
            get => _timeWorked;
            set
            {
                _timeWorked = Mathf.Clamp(value, 0, MAXFATIGUE);
                OnTimeWorkedChanged?.Invoke(this, _timeWorked);
            }
        }

        public event EventHandler<float> OnDurabilityChanged;

        public float ToolDurability
        {
            get => _toolDurability;
            set
            {
                _toolDurability = Mathf.Clamp(value, 0, MAXTOOLDURABILITY);
                OnDurabilityChanged?.Invoke(this, _toolDurability);
            }
        }


        public void OnFinishedAction()
        {
            doingAction = false;
            aiBrain.DecideBestAction(actionsAviable);
        }

        private void Start()
        {
            mover = GetComponent<MoveController>();
            aiBrain = GetComponent<AIBrain>();
            aiBrain.DecideBestAction(actionsAviable);
        }

        private void Update()
        {
            if (aiBrain.finishedDeciding)
            {
                aiBrain.finishedDeciding = false;
                Debug.Log("Ejecutando");
                aiBrain.bestAction.Execute(this);
            }
        }

        #region Corutines

        public void Rest()
        {
            Debug.Log("REST");

            mover.MoveTo(HousePosition.position);
            actionImage.sprite = BocadillosSistema._instance.irACasa;

            Debug.Log("Lanzo corutina");
            StartCoroutine(RestCoroutine());
        }


        public void GetPaid()
        {
            mover.MoveTo(HousePosition.position);
            actionImage.sprite = BocadillosSistema._instance.NecesitoPaga;
            PayButton.enabled = true;
            dineroNecesarioAPagar = (int) ((MAXTOOLDURABILITY - _toolDurability) * 0.2f);
            DineroAPagar.text = "I need  " + dineroNecesarioAPagar.ToString() + " Bricks to keep working";

            //Ha llegado el pibe ya a casa a cobrar? Tengo que poner aqu� la animaci�n?

            //StartCoroutine()
        }


        public void PagarAlPibe()
        {
            if (PlayerStats._instance.gold >= dineroNecesarioAPagar)
            {
                PlayerStats._instance.gold -= dineroNecesarioAPagar;
                dineroNecesarioAPagar = 0;
                ToolDurability = MAXTOOLDURABILITY;
                PayButton.enabled = false;
                OnFinishedAction();
            }

            PanelDePago.SetActive(false);
        }


        public void Patrullar()
        {
            mover.MoveTo(WayPointsPatrol[waypoint].position);
            waypoint++;
            if (waypoint >= WayPointsPatrol.Length)
                waypoint = 0;
            StartCoroutine(PatrolCoroutine());
        }


        public void Aparecer()
        {
            StartCoroutine(Reaparecer());
        }

        public void Repair()
        {
            Debug.Log("Ha currar");
            List<PlacedBuild> damagedBuilds = GridBuildingSystem.Instance.BuildStruct.Values.ToList();
            int dmgBuildIdx = -1;

            for (int i = 0; i < damagedBuilds.Count; i++)
            {
                PlacedBuild build = damagedBuilds[i];

                if (!build.isDamaged) continue;

                dmgBuildIdx = i;
                break;
            }

            if (dmgBuildIdx == -1)
            {
                Debug.LogWarning("No se ha encontrado edificio roto");
                return;
            }

            mover.MoveTo(GridBuildingSystem.Instance.GetWorldCenter(damagedBuilds[dmgBuildIdx]));
            actionImage.sprite =
                BocadillosSistema._instance.GetEdificioCereza(damagedBuilds[dmgBuildIdx].BuildingSo.name);
            StartCoroutine("RepairCoroutine", damagedBuilds[dmgBuildIdx]);
        }

        IEnumerator RepairCoroutine(PlacedBuild build)
        {
            while (mover.reached != true)
            {
                yield return new WaitForSeconds(0.5f);

                Debug.Log("De camino y tal");
            }

            Debug.Log("Reparando");
            characterAnimator.SetBool("Repair", true);
            yield return new WaitForSeconds(4);
            build.BuildRepair(this);
            characterAnimator.SetBool("Repair", false);
            OnFinishedAction();
        }


        IEnumerator Reaparecer()
        {
            characterAnimator.SetBool("Land", true);
            yield return new WaitForSeconds(6);
            characterAnimator.SetBool("Land", false);
            OnFinishedAction();
        }

        IEnumerator Desaparecer()
        {
            Debug.Log("Animacion y tal");
            characterAnimator.SetBool("TakeOff", true);
            yield return new WaitForSeconds(6);
            characterAnimator.SetBool("TakeOff", false);
            gameObject.SetActive(false);
        }


        IEnumerator PatrolCoroutine()
        {
            while (mover.reached != true)
            {
                yield return new WaitForSeconds(0.5f);

                Debug.Log("De camino y tal");
            }

            yield return new WaitForSeconds(0);
            OnFinishedAction();
        }

        IEnumerator RestCoroutine()
        {
            while (mover.reached != true)
            {
                yield return new WaitForSeconds(0.5f);

                Debug.Log("De camino y tal");
            }

            characterAnimator.SetBool("Rest", true);
            actionImage.sprite = BocadillosSistema._instance.irADormir;
            Debug.Log("ZZZZZzzz");
            sleepTime = MAXFATIGUE - _timeWorked;
            sleepTime /= 10;
            sleepTime *= 0.5f;
            yield return new WaitForSeconds(sleepTime);
            Debug.Log("Siesta completada");
            TimeWorked = 0;
            characterAnimator.SetBool("Rest", false);
            OnFinishedAction();
        }

        IEnumerator GetPaidCoroutine()
        {
            while (mover.reached != true)
            {
                yield return new WaitForSeconds(0.5f);

                Debug.Log("De camino y tal");
            }
        }

        #endregion

        [ContextMenu("Pagar")]
        public void Pagar()
        {
            ToolDurability = MAXTOOLDURABILITY;
            OnFinishedAction();
        }
    }
}