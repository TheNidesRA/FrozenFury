using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AutoAttackScripts
{
    public class turretRotation : AutoShoot
    {
        private int _timeAlive = 4;

        public override float Damage
        {
            get => 3;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                damage = 3;
            }
        }


        public override void RotatePlayerToEnemy(GameObject enemy)
        {
            if (enemy == null) return;
            player.transform.LookAt(enemy.transform);
        }

        public override void StartStopShooting()
        {
            if (IsShootingEnabled())
            {
                GetComponent<Collider>().enabled = false;
                ClearEnemies();
            }
            else
            {
                GetComponent<Collider>().enabled = true;
            }
        }
    }
}