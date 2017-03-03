using System.Collections.Generic;

public class SkillModel {


    /// <summary>
    /// int-careerId
    /// </summary>
    private static Dictionary<int, SkillItem> skillDict = new Dictionary<int, SkillItem>();

    public static void SetSkillItem(int careerId,SkillItem item)
    {
        skillDict[careerId] = item;
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

}
