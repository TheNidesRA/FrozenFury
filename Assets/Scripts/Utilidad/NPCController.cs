using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using GridSystem;
using UnityEngine;

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


        [SerializeField] private float _timeWorked;

        [SerializeField] private float _toolDurability;

        public float TimeWorked
        {
            get => _timeWorked;
            set { _timeWorked = Mathf.Clamp(value, 0, MAXFATIGUE); }
        }

        public float ToolDurability
        {
            get => _toolDurability;
            set { _toolDurability = Mathf.Clamp(value, 0, MAXTOOLDURABILITY); }
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

            Debug.Log("Lanzo corutina");
            StartCoroutine(RestCoroutine());
        }


        public void GetPaid()
        {
            mover.MoveTo(HousePosition.position);
            //StartCoroutine()
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

            mover.MoveTo(damagedBuilds[dmgBuildIdx].transform.position);
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
            yield return new WaitForSeconds(0);
            build.BuildRepair(this);
            OnFinishedAction();
        }

        IEnumerator RestCoroutine()
        {
            while (mover.reached != true)
            {
                yield return new WaitForSeconds(0.5f);

                Debug.Log("De camino y tal");
            }

            Debug.Log("ZZZZZzzz");
            yield return new WaitForSeconds(3);
            Debug.Log("Siesta completada");
            TimeWorked -= 10;
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