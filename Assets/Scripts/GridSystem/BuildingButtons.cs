using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;

public class BuildingButtons : MonoBehaviour
{
   [SerializeField]private PlacedBuild _placedBuild;
   
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

   public void Remove()
   {
    GridBuildingSystem.Instance.RemoveBuild(_placedBuild);
   }

   public void Close()
   {
      _placedBuild.DisableCanvas2();
   }
   
   
}
