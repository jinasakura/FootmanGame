using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{

    void Update()
    {
        //transform.LookAt(transform.position + lookedCamera.transform.rotation * Vector3.forward,
        //    lookedCamera.transform.rotation * Vector3.up);
        Vector3 fwd = Camera.main.transform.forward;
        fwd.y = 0;
        transform.rotation = Quaternion.LookRotation(fwd);
    }
}