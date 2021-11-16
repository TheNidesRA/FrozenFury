using TheKiwiCoder;

namespace Nodes.DiablilloTree
{
    public class PlayerExistNode : ActionNode
    {
        public PlayerExistNode(){}

        protected override void OnStart()
        {
            // throw new System.NotImplementedException();
        }

        protected override void OnStop()
        {
            // throw new System.NotImplementedException();
        }

        protected override State OnUpdate()
        {
            return (PlayerStats._instance == null) ? State.Failure : State.Success;
        }
    }
}