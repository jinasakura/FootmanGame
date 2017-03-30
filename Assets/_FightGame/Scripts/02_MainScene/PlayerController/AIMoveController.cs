using UnityEngine;
using System.Collections;

public class AIMoveController : RoleMoveController
{

    protected override void MoveMode()
    {
        //float h = Random.Range(PlayerDetail.StayOffset, 0.5f);
        //float v = Random.Range(PlayerDetail.StayOffset, 0.5f);
        //character.Move(h, v);
    }

    protected override void TurnMode()
    {
        //float offset = Random.Range(10, 90);
        //PerformBodyRotation(offset);
    }


}
