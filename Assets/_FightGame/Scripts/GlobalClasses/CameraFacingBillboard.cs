using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{
    private Camera faceCamera;

    //void Start()
    //{
    //    faceCamera = Camera.main;
    //}

    void Update()
    {
        if (Camera.main != null)
        {
            
            Vector3 fwd = Camera.main.transform.forward;
            //Debug.Log(fwd);
            //fwd.y = 0;
            transform.rotation = Quaternion.LookRotation(fwd);
        }
        
    }
}