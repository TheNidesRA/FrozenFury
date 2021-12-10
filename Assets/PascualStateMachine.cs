using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.UI;

public class PascualStateMachine : MonoBehaviour
{
    public Enemy Pascual;
    [SerializeField] private State currentState;
    public Animator animator;
    public GameObject bullet;
    private float duration;
    private float startTime;
    public GameObject BuffArea;
    private bool AreaActive = false;
    [Range(0f,1f)]
    public float PorcentajeVidaBoost;

    public Sprite chasing;
    public Sprite van;

    private float vidaBoost;

    private enum State
    {
        ChassingPlayer,
        GoToVan,
        Attack,
        AttackAnimation
    }

    private void Start()
    {
        AreaActive = false;
        Evaluate();
        vidaBoost = Pascual.health * PorcentajeVidaBoost;
    }

    private void Update()
    {
        RunStateMachine();
        
        if (Pascual.health <= (vidaBoost) && !AreaActive)
        {
            BuffArea.SetActive(true);
            AreaActive = true;
        }
            
    }


    private void RunStateMachine()
    {
        switch (currentState)
        {
            case State.GoToVan:

                Evaluate();
                break;
            case State.ChassingPlayer:

                float distance = Vector3.Distance(PlayerStats._instance.transform.position, transform.position);
                if (distance < Pascual.attackRange)
                {
                   // Debug.Log("Hemos llegao: " + distance);
                    Pascual.NavMeshAgent.isStopped = true;
                    currentState = State.Attack;
                }
                else
                {
                    Evaluate();
                }

                break;
            case State.Attack:
               // Debug.Log("Attack");
                duration = 1 / Pascual.attackSpeed;
                startTime = Time.time;
                animator.SetBool("Attack", true);
                animator.SetFloat("SpeedMult", Pascual.attackSpeed);
                bullet.GetComponent<ChasePlayerBullet>().bulletDamage = Pascual.damage;
                var bulletBunny = Instantiate(bullet, transform.position, Quaternion.identity);
                Destroy(bulletBunny, 2);
                currentState = State.AttackAnimation;
                break;
            case State.AttackAnimation:
                if (Time.time - startTime > duration)
                {
                  //  Debug.Log("AttackCD");
                    animator.SetBool("Attack", false);
                    Evaluate();
                }

                break;
        }
    }

    private void Evaluate()
    {
        animator.SetBool("Run", true);
        if (PlayerStats._instance.Health > 0)
        {
            //Debug.Log("A chasear ");
            Pascual.ActionImage.sprite = BocadillosSistema._instance.GetPlayer();;
            //Pascual.NavMeshAgent.ResetPath();
            currentState = State.ChassingPlayer;
            Pascual.NavMeshAgent.SetDestination(PlayerStats._instance.transform.position);
            Pascual.NavMeshAgent.isStopped = false;
        }
        else
        {
            // Pascual.NavMeshAgent.ResetPath();
            Pascual.ActionImage.sprite = van;
            currentState = State.GoToVan;
            Pascual.NavMeshAgent.SetDestination(EnemyGoal.instance.getPosition());
            Pascual.NavMeshAgent.isStopped = false;
        }
    }
}