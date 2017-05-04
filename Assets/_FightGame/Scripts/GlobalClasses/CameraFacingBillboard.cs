using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{
    private Camera faceCamera;

    void Start()
    {
        faceCamera = Camera.main;
    }

    void Update()
    {
        Vector3 fwd = faceCamera.transform.forward;
        //Debug.Log(fwd);
        //fwd.y = 0;
        transform.rotation = Quaternion.LookRotation(fwd);
    }
}