using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBuild : MonoBehaviour
{
   public GameObject canvas;



   public void EnableCanvas()
   {
      canvas.SetActive(true);
   }

   public void DisableCanvas()
   {
      canvas.SetActive(false);
   }
}
