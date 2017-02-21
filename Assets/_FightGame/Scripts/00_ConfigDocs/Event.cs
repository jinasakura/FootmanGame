using UnityEngine;
using System.Collections;

public class LoginEvent  {

    public static string DataIsReady = "DataIsReady";

    public static string SelectedCareer = "SelectedCareer";

}

public class MainSceneEvent
{
    public static string TriggerSkill = "TriggerSkill";

    public static string CharacterDie = "CharacterDie";

    public static string TakeDamage = "TakeDamage";
}

public class StateMachineEvent
{
    public static string HandleParamers = "HandleParamers";

    public static string OnceActionBegain = "OnceActionBegain";

    public static string OnceActionEnd = "OnceActionEnd";

    public static string OnceActionChange = "OnceActionChange";
}


