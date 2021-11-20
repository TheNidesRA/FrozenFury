using System;
using System.Collections;
using System.Collections.Generic;
using AutoAttackScripts;
using Enemies;
using UnityEngine;

public class ChasePlayerBullet : MonoBehaviour
{
    Rigidbody rb;

    public float speed = 0.02f;

    private Transform target;

    private float moveSpeedFollow = 1f;

    public float rotateSpeed = 200f;
    [HideInInspector] public float bulletDamage;

    private AutoShoot Player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = FindObjectOfType<AutoShoot>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        // float interpolationRatio = (float)(elapsedFrames / interpolationFramesCount);
        // Vector3 interpolatedPosition =
        //     Vector3.Lerp(Vector3.up, PlayerStats._instance.transform.position, interpolationRatio);
        Vector3 direction = target.position - transform.position;

        direction.Normalize();

        Vector3 rotateAmount = Vector3.Cross(direction, transform.forward);

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.forward * speed;
        rb.AddForce(direction, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("ChasingPlayer")) return;
        PlayerStats._instance.Health -= bulletDamage;
        Destroy(gameObject);
    }
}