public class SkillRef  {

    /// <summary>
    /// ClosePerson--近距离接触攻击单人
    /// UniObjectPerson--单发射物攻击单人,单发射物远程大范围攻击
    /// MutiObjectPerson--多发射物攻击单人
    /// MutiObjectPeople--多发射物攻击多人
    /// UntouchPeopleInCloseScope--近距离范围内非接触攻击多人
    /// TouchPersonInCloseScope--近距离范围内接触攻击多人
    /// CloneSelf--克隆自己进行攻击
    /// </summary>
    public enum Attack
    {
        TouchPerson = 1, UniObject, MutiObjectPerson, MutiObjectPeople, UntouchPeopleInCloseScope,
        TouchPeopleInCloseScope, CloneSelf
    };

    //CureSelf-治愈自己，CureOther-治愈他人（这个人离我的远近？），DefenseSelf-自己加防御，DefenseGroup-防御某范围内一群人
    public enum Passive { CureSelf = 1, CureOther, CureGroup, DefenseSelf, DefenseGroup };

    public enum SkillType { TakeDamage };

    public static string WeaponTag = "Weapon";

    public static string BodyTag = "Body";

    public static string FirePointTag = "FirePoint";

    public static string PlayersLayer = "Players";

    public static string AILayer = "AI";

    public static string EnvironmentLayer = "Environment";

    

    


}
