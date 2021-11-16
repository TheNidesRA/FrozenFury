using TheKiwiCoder;

namespace Nodes.DiablilloTree
{
    public  class PlayerAlive : ActionNode
    {
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
            return PlayerStats._instance.IsPlayerAlive() ? State.Success : State.Failure;
        }
    }
}