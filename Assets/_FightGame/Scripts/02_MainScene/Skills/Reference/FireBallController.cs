using UnityEngine;
using System.Collections;

public class FireBallController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody rb;
    private int playerId;
    private float range;
    private Vector3 startPoint;

    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    //Transform direction = GetComponentInParent<RoleSkillController>().gameObject.transform;
    //    //rb.velocity = direction.forward * speed;
    //    //startPoint = gameObject.transform.position;
    //}

    public void Fire(Transform start)
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = start.forward * speed;
        startPoint = start.position;
        Debug.Log("velocity->" + rb.velocity + "   startPoint->" + startPoint);
    }

    public void SetOwnerId(int id)
    {
        playerId = id;
    }

    public void SetRange(float r)
    {
        range = r;
    }

    void FixedUpdate()
    {
        Vector3 endPoint = gameObject.transform.position;
        float distance = (endPoint - startPoint).magnitude;
        //Debug.Log(distance+"      "+range);
        if (distance > range)
        {
            //Debug.Log("消失！         " + distance);
            Destroy(gameObject);
        }
        //else
        //{

        //}
    }

}
