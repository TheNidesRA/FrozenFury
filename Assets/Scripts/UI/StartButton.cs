using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void StartRound()
    {
        WaveController._instance.StartWave();
    }
}
