using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
  

  public static void PauseGameAction()
  {
    Time.timeScale = 0;
  }
  
  public static void ResumeGameAction()
  {
    Time.timeScale = 1;
  }

  
  
}
