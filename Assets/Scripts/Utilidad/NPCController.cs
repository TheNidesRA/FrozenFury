using System;
using System.Collections;
using Enemies;
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
            set { _timeWorked = Mathf.Clamp(value, 0, 100); }
        }

        public float ToolDurability
        {
            get => _toolDurability;
            set { _toolDurability = Mathf.Clamp(value, 0, 100); }
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
            mover.MoveTo(EnemyGoal.instance.getPosition());
            //StartCoroutine()
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
    }
}