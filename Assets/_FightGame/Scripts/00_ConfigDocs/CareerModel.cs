using System.Collections.Generic;
public class CareerModel
{

    private static Dictionary<int, CareerItem> careerDict = new Dictionary<int, CareerItem>();

    public static void SetCareerItem(int careerId, CareerItem item)
    {
        careerDict[careerId] = item;
    }

    public static int GetCareerCount()
    {
        return careerDict.Count;
    }

    public static Dictionary<int,CareerItem>.ValueCollection GetAllCareerItem()
    {
        return careerDict.Values;
    }

    public static CareerLevelItem GetLevelItem(int careerId, int level)
    {
        CareerItem careerItem = careerDict[careerId];
        CareerLevelItem desLevel = null;
        foreach (CareerLevelItem item in careerItem.levels)
        {
            if (item.id == level)
            {
                desLevel = item;
                break;
            }
        }
        return desLevel;
    }

    public static CareerItem GetCareerItem(int careerId)
    {
        return careerDict[careerId];
    }

}
