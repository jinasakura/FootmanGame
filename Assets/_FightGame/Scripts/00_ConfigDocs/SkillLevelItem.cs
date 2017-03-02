
public class SkillLevelItem
{
    public int id;
    public string name;

    //消耗hp
    public int damage;

    //技能限制等级，对应CareerLevelItem里的id
    public int level;
    public int mp;

    //0-主动技能-false ; 1-被动技能-true
    public bool passive;

}
