using System;

using UnityEngine;

namespace UtilityBehaviour
{
    public class NPCController : MonoBehaviour
    {
        public MoveController mover { get; set; }
        public AIBrain aiBrain { get; set; }
        public UtilityAction[] actionsAviable;


        private void Start()
        {
            mover = GetComponent<MoveController>();
            aiBrain = GetComponent<AIBrain>();

        }
    }
}

   
