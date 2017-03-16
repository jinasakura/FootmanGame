using System.Collections.Generic;

public class SkillTpye
{
    public enum Attack { Single=0,Group,Ground };
}

public class SkillLevelItem
{
    public int id;
    public int careerId;
    public string skillName;

    //消耗敌人hp
    public float damageHp;

    //技能限制等级，对应CareerLevelItem里的id
    public int level;
    public float mp;

    //0-主动技能-false ; 1-被动技能-true
    public bool passive;

    //1-单人；2-群人；3-对地
    public int skillType;

}

public class SkillItem
{

    public int careerId;
    public SkillLevelItem[] levels;

    public List<SkillLevelItem> GetSkillLevels(int level)
    {
        List<SkillLevelItem> skillLevels = new List<SkillLevelItem>();
        foreach (SkillLevelItem item in levels)
        {
            if (item.level <= level)
            {
                skillLevels.Add(item);
            }
            else
            {
                break;
            }
        }
        return skillLevels;
    }
}

public class SkillModel {
    /// <summary>
    /// int-careerId
    /// </summary>
    private static Dictionary<int, SkillItem> skillDict = new Dictionary<int, SkillItem>();
    private static Dictionary<string, SkillLevelItem> skillInfoDict = new Dictionary<string, SkillLevelItem>();

    public static void SetSkillItem(int careerId,SkillItem item)
    {
        skillDict[careerId] = item;
    }

    public static void SetSkillInfoByName(string skillName,SkillLevelItem item)
    {
        skillInfoDict[skillName] = item;
    }

    public static List<SkillLevelItem> GetAllSkillLevels(int careerId,int level)
    {
        SkillItem item = skillDict[careerId];
        return item.GetSkillLevels(level);
    }

    public static SkillLevelItem GetSkillById(int careerId,int skillId)
    {
        SkillItem skillItem = skillDict[careerId];
        SkillLevelItem level = null;
        foreach (SkillLevelItem item in skillItem.levels)
        {
            if(item.id == skillId)
            {
                level = item;
                break;
            }
        }
        return level;
    }

    public static SkillLevelItem GetSkillLevelByName(string skillName)
    {
        SkillLevelItem result;
        if (skillInfoDict.TryGetValue(skillName, out result))
        {
            return result;
        }
        return null;
    }

}
