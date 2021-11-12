using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.UIElements;

namespace Enemies
{
    public class EnemyHealthbar : MonoBehaviour
    {
        private float maxHealth;
        public UnityEngine.UI.Image healtBar;
        // private GameObject Camera;
        private void Awake()
        {
            // Camera = GameObject.FindWithTag("MainCamera");
            Enemy enemy = GetComponentInParent<Enemy>();
            // healtBar = GetComponentInChildren<UnityEngine.UI.Image>();
            maxHealth = enemy.health;
            // gameObject.transform.LookAt(Camera.transform.position);
        }


        public void HandleHealth(float hp)
        {
            healtBar.fillAmount = hp / maxHealth;
        }
        
    }
}

