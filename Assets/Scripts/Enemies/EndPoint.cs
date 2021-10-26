using System;
using UnityEngine;

namespace Enemies
{
    public class EndPoint : MonoBehaviour
    {
        /// <summary>
        /// Class incharged of controlling the rounds
        /// </summary>

        public void OnCollisionEnter(Collision other)
        {
            if(!other.gameObject.CompareTag("Enemy")) return;
            
            Destroy(other.gameObject);
        }
        
    }
}