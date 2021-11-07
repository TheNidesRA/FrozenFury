using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class Sequence : Node
    {
        protected List<Node> nodes = new List<Node>();

        public Sequence(List<Node> nodes)
        {
            this.nodes = nodes;
        }
        
        public override NodeState Evaluate()
        {
            bool isAnyChildRunning = false;
            foreach (Node node in nodes)
            {
                switch (node.Evaluate())
                {
                    //If any childNode inside a sequence is a failure, the whole sequence would be so
                    case NodeState.FAILURE:
                        _nodeState = NodeState.FAILURE;
                        return _nodeState;

                    case NodeState.RUNNING:
                        isAnyChildRunning = true;
                        break;
                    
                    //If a childNode is a success, we just keep evaluating the children
                    case NodeState.SUCCESS:
                        break;
                }
            }

            _nodeState = isAnyChildRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return _nodeState;
        }
    }
}
