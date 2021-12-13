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

        public float TimeWorked
        {
            get => _timeWorked;
            set { _timeWorked = Mathf.Clamp(value, 0, 100); }
        }

        [SerializeField] private float _timeWorked;

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

        public void DoWork(int time)
        {
            StartCoroutine(WorkCoroutine(time));
        }

        public void Rest()
        {
            Debug.Log("REST");

            mover.MoveTo(EnemyGoal.instance.getPosition());

            Debug.Log("Lanzo corutina");
            StartCoroutine(RestCorutine());
        }


        IEnumerator RestCorutine()
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

        IEnumerator WorkCoroutine(int time)
        {
            // int counter = time;
            //
            // while (counter > 0)
            // {
            //     yield return new WaitForSeconds(1);
            //     counter--;
            // }
            //
            // Debug.Log("Fin y tal del trabajito");

            yield return new WaitForSeconds(0);
            while (!mover.reached)
            {
                Debug.Log("De camino y tal");
            }

            callback.Invoke();
            Debug.Log("Ale descansando");
            OnFinishedAction();
        }

        #endregion
    }
}