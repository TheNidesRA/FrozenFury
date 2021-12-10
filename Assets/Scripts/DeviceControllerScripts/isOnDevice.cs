using UnityEngine;

public class isOnDevice : MonoBehaviour
{
    public GameObject mobileCanvas;

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
        if (isMobile == true)
            mobileCanvas.SetActive(true);
        else
            mobileCanvas.SetActive(false);
#endif
    }

    private void Start()
    {
        
    }


    private void Awake()
    {
        CheckIfMobile();
    }
}