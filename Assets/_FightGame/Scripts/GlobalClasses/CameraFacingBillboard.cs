using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{

    void Update()
    {
        Vector3 fwd = Camera.main.transform.forward;//注意这个Camera.main可能不是想要的摄像机
        fwd.y = 0;
        transform.rotation = Quaternion.LookRotation(fwd);
    }
}