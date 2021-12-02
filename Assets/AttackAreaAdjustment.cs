using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaAdjustment : MonoBehaviour
{
   public GameObject Area;
   public GameObject Edificio;
   public SphereCollider Collider;
   public bool prefab = false;


   private void Awake()
   {
      if (prefab)
      {
     
         Vector3 scalev = new Vector3(Edificio.transform.localScale.x,1,Edificio.transform.localScale.z) * Collider.radius;
         scalev.y = 0.5f;
         Area.transform.localScale = scalev;
         return;
      }
      
      Transform Scale = Edificio.transform.Find("Pivote");

      Collider = Scale.GetComponent<SphereCollider>();
      
      Vector3 scale = new Vector3(Scale.localScale.x,1,Scale.localScale.z) * Collider.radius;
      scale.y = 0.5f;
      Area.transform.localScale = scale;
      
    
      
      
      
   }
}
