using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectCharacter : MonoBehaviour
{
  public RectTransform canvas;
  public GameObject Container;

  public List<GameObject> Characters;
  
  
  public void SelectCharacter(int xPosition)
  {

    for (var i = 0; i < Characters.Count; i++)
    {
        if (xPosition == i)
        {
            Characters[i].SetActive(true);
        }
        else
        {
            Characters[i].SetActive(false); 
        }
    }

     Container.SetActive(true);
     gameObject.SetActive(false);

  }
}
