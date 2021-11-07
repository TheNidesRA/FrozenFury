using System.Collections.Generic;

namespace Nodes
{
    public class Selector : Node
    {
        private List<Node> nodes = new List<Node>();
        
        public Selector(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        public override NodeState Evaluate()
        {
            foreach (Node node in nodes)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        break;

                    case NodeState.RUNNING:
                        _nodeState = NodeState.RUNNING;
                        return _nodeState;
                    
                    case NodeState.SUCCESS:
                        _nodeState = NodeState.SUCCESS;
                        return _nodeState;
                }
            }

            _nodeState = NodeState.FAILURE;
            return _nodeState;
        }
    }
}