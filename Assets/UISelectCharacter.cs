using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectCharacter : MonoBehaviour
{
  public RectTransform canvas;
  public GameObject Container;

  public void SelectCharacter(float xPosition)
  {
    
    Vector3 pos = canvas.localPosition;
    pos.x = xPosition;
    canvas.localPosition = pos;
    //canvas.gameObject.SetActive(true);
    Container.SetActive(true);
    gameObject.SetActive(false);
  }
}
