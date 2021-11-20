using TheKiwiCoder;
using UnityEngine;

public class EnemyBunnyHitsPlayer : ActionNode
{
    public GameObject bullet;

    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemyBunnyHitsPlayer";
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Collider[] colliders =
            Physics.OverlapSphere(context.transform.position, context.enemy.attackRange,
                LayerMask.GetMask("Player"));

        if (colliders.Length > 0)
        {
            // PlayerStats._instance.Health -= context.enemy.damage;
            // Debug.Log(" Vida : " + PlayerStats._instance.Health);
            Debug.Log("Se entro");
            bullet.GetComponent<ChasePlayerBullet>().bulletDamage = context.enemy.damage;
            var bulletBunny = Instantiate(bullet, context.transform.position, Quaternion.identity);
            Destroy(bulletBunny, 2);
        }

        return State.Success;
    }
}