namespace Nodes
{
    public class Invertor : Node
    {
        protected Node node;

        public Invertor(Node node)
        {
            this.node = node;
        }
        public override NodeState Evaluate()
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    _nodeState = NodeState.SUCCESS;
                    break;
                    
                case NodeState.SUCCESS:
                    _nodeState = NodeState.FAILURE;
                    break;

                case NodeState.RUNNING:
                    _nodeState = NodeState.RUNNING;
                    break;
            }

            return _nodeState;
        }
    }
}