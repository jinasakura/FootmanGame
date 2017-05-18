using UnityEngine;
using System.Collections.Generic;

public class AITypeItem
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
    public static Dictionary<int, AITypeItem> aiDict = new Dictionary<int, AITypeItem>();

    public static GameObject[] wayPoints;

    public static GameObject AIRoleBasePrefab;

    public static void SetAiType(int id,AITypeItem info)
    {
        aiDict[id] = info;
    }

    public static AITypeItem GetAiTypeInfo(int id)
    {
        return aiDict[id];
    }
}
