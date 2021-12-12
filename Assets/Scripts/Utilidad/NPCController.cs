using System;
using System.Collections;
using UnityEngine;

namespace UtilityBehaviour
{
    public class NPCController : MonoBehaviour
    {
        public MoveController mover { get; set; }
        public AIBrain aiBrain { get; set; }
        public UtilityAction[] actionsAviable;

        public float TimeWorked
        {
            get => _timeWorked;
            set
            {
                _timeWorked = Mathf.Clamp(value, 0, 100);
            }
        }

        private float _timeWorked;


        private void Start()
        {
            mover = GetComponent<MoveController>();
            aiBrain = GetComponent<AIBrain>();
        }

        #region Corutines

        public void DoWork(int time)
        {
            StartCoroutine(WorkCoroutine(time));
        }


        IEnumerator WorkCoroutine(int time)
        {
            int counter = time;

            while (counter > 0)
            {
                yield return new WaitForSeconds(1);
                counter--;
            }

            Debug.Log("Fin y tal del trabajito");
        }

        #endregion
    }
}