
using UnityEngine;


[ExecuteInEditMode]
public class MoveWhenIncrease : MonoBehaviour
{
    public RectTransform texto;
    private RectTransform _rect;


    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }
  
    void Update()
    {
        float x = texto.rect.width / 2;

        Vector3 pos = new Vector3(x, _rect.localPosition.y, _rect.localPosition.z);
        Debug.Log(pos);
        _rect.localPosition = pos;
    }
}
