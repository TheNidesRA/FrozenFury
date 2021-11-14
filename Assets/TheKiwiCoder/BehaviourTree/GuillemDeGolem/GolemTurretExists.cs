using UnityEngine;
using TheKiwiCoder;

public class GolemTurretExists : ActionNode
{
    private PlacedBuild turret;
    private bool fail = false;

    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "GolemTurretExists";
        fail = false;
        if (context.enemy.auxActionTarget!=null)
        {
            Debug.Log(context.enemy.auxActionTarget);
            Debug.Log("Que pasa aqui: "+ReferenceEquals(context.enemy.auxActionTarget, null));
            if (context.enemy.auxActionTarget.TryGetComponent<PlacedBuild>(out PlacedBuild build))
            {
                if (build.BuildingSo.type == BuildingSO.BuildingType.Turret)
                {
                    turret = build;
                    context.enemy.actionTarget = turret.gameObject;
                    context.enemy.auxActionTarget = null;
                    Debug.Log("A por la torretilla despues dle muro");
                }
                else
                {
                    fail = true;
                }
            }
            else
            {
                fail = true;
            }
        }
        else if(context.enemy.actionTarget != null)
        {
            if (context.enemy.actionTarget.TryGetComponent<PlacedBuild>(out PlacedBuild build))
            {
                if (build.BuildingSo.type == BuildingSO.BuildingType.Turret)
                {
                    //turret = build;
                    Debug.Log("No habia muro a por la torretilla directo");
                }
                else
                {
                    fail = true;
                }
            }
            else
            {
                fail = true;
            }
        }
        else
        {
            fail = true;
        }
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return fail ? State.Failure : State.Success;
    }
}