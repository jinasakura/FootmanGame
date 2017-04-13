using UnityEngine;
using System.Collections.Generic;

public class LanchersController  {

    public GameObject[] lanchers;

    private static Dictionary<string, GameObject> lancherDict = new Dictionary<string, GameObject>();

    public static void SetLancher(string lancherName,GameObject lancher)
    {
        lancherDict[lancherName] = lancher;
    }

    public static GameObject GetLancherByName(string lancherName)
    {
        return lancherDict[lancherName];
    }

}
