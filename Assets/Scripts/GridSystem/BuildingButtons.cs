using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;

public class BuildingButtons : MonoBehaviour
{


   public void Rotate()
   {
      GridBuildingSystem.Instance.Rotate();
   }

   public void EnableMove()
   {
      GridBuildingSystem.Instance.Mover();
   }

   public void Confirm()
   {
      GridBuildingSystem.Instance.Confirm();
         
   }
  
   
}
