using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    [System.Serializable]
    public abstract class Node
    {
        /// <summary>
        /// Every NODE will be between 3 different states while going through the tree
        /// </summary>
        protected NodeState _nodeState;
        public NodeState nodeState { get { return _nodeState; } }
        /// <summary>
        /// This class will be implemented for each NODE
        /// </summary>
        /// <returns></returns>
        public abstract NodeState Evaluate();
    }

    public enum NodeState
    {
        RUNNING, SUCCESS, FAILURE,
    }
}

