using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Enemies;
using UnityEngine;

public class EnemyPascual : Enemy
{
    public PascualStateMachine psm;
    
    
    public GameObject boost;

    public override void Die()
    {
        psm.enabled = false;
        base.Die();
    }
}
