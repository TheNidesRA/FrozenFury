using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class isOnDevice : MonoBehaviour
{
    public GameObject mobileCanva;

    // Start is called before the first frame update

    bool isMobile;

#if !UNITY_EDITOR && UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    static extern bool IsMobile();
#endif

    void CheckIfMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
            isMobile = IsMobile();
            if(isMobile == true)
                mobileCanva.SetActive(true);
            else
                mobileCanva.SetActive(false);
#endif
    }


    private void Awake()
    {
        //CheckIfMobile();
       
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
