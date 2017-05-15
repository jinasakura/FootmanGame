/// <summary>
/// 
/// </summary>
public class RoleRef  {

    //以下状态机相关

    public static float STAY_OFFSET = 0.001f;//区分站立和走的临界数

    public enum StayStateType { Idle, Victory, Upset, Defend };

    public static string isLive = "isLive";
    public static string stayState = "stayState";
    public static string skillId = "skillId";
    public static string speed = "speed";
    public static string isSkill = "isSkill";
    public static string triggerSkill = "triggerSkill";

}
