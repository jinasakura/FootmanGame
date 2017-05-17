using UnityEngine;
using System.Collections.Generic;

public class AIType
{
    public int typeId;
    public bool stay;
    public int waypointIndex;
    public float patrolSpeed;
    public float chaseSpeed;
    public float waypointDistance;
    public float warnRadius;
    public float patrolGapTime;
    public float cameraFar;
}

public class AIModel  {
    public static Dictionary<int, AIType> aiDict = new Dictionary<int, AIType>();

    public static GameObject[] wayPoints;

    public static GameObject AIRoleBasePrefab;

    public static void SetAiType(int id,AIType info)
    {
        aiDict[id] = info;
    }

    public static AIType GetAiType(int id)
    {
        return aiDict[id];
    }
}
