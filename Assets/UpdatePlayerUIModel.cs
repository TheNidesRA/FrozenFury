using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdatePlayerUIModel : MonoBehaviour
{

    public GameObject SpawnPoint;
    public TextMeshProUGUI name;
    private Transform buildUpdate;

    void Start()
    {
        name.text = PlayerStats._instance.PlayerStatsSo.name;
        
       GameObject buildUpdate = Instantiate(PlayerStats._instance.PlayerStatsSo.CanvasModel.gameObject, Vector3.zero, Quaternion.identity,
           SpawnPoint.transform);
       buildUpdate.transform.localPosition = PlayerStats._instance.PlayerStatsSo.CanvasModel.position;
       buildUpdate.transform.localEulerAngles = Vector3.forward;
       
       // buildUpdate.localPosition = new Vector3(0, 0, -60);
       // SpawnPoint.localEulerAngles = buildUpdate.BuildingSo.canvasVisual.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
