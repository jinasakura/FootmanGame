using UnityEngine;
using System.Collections;

public class FireBallController : MonoBehaviour {

    protected Rigidbody rb;
    protected int _playerId;
    public int playerId { get; set; }
    protected Vector3 startPoint;
    private SkillLevelItem _skillInfo;
    public SkillLevelItem skillInfo { get; set; }

    //protected LayerMask damageMask;
    //public float explosionForce;

    //void Start()
    //{
    //    damageMask = LayerMask.NameToLayer(SkillRef.PlayersLayer);
    //}

    public void Fire(Vector3 forward,Vector3 position)
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = forward * skillInfo.releaseSpeed;
        startPoint = position;
        //Debug.Log("velocity->" + rb.velocity + "   startPoint->" + startPoint);
    }

}
