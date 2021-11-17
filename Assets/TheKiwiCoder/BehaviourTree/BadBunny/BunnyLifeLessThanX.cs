using System;
using Enemies;
using TheKiwiCoder;
using UnityEngine;

public class BunnyLifeLessThanX : ActionNode
{

    private float _threshold;
    public float percentageLife = 0.2f;
    private bool primera=true;

    protected override void OnStart()
    {
        if (primera)
        {
            Debug.Log("asda");
            _threshold = context.enemy.health * percentageLife;
            primera = false;
        }
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
//        Debug.Log("Vida : "+context.enemy.health+" con el tal "+ _threshold );
//        Debug.Log((context.enemy.Health < _threshold));
        return context.enemy.health < _threshold ? State.Success : State.Failure;
    }
}