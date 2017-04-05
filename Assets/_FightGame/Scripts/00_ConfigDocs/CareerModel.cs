using System.Collections.Generic;

//public class CareerLevelItem
//{
//    public int id;
//    public int exp;
//    public int maxHp;
//    public int maxMp;
//}

public class CareerItem
{
    public int careerId;
    public string careerName;
    public string modelName;
    public int level;
    public float exp;
    public float maxHp;
    public float maxMp; 

    //public CareerLevelItem[] levels;

    //public CareerLevelItem GetCareerLevel(int level)
    //{
    //    CareerLevelItem careerLevel = null;
    //    foreach (CareerLevelItem item in levels)
    //    {
    //        if (item.id == level)
    //        {
    //            careerLevel = item;
    //            break;
    //        }
    //    }
    //    return careerLevel;
    //}
}

public class CareerModel
{
    private static Dictionary<int, List<CareerItem>> careerDict = new Dictionary<int, List<CareerItem>>();
    private static List<CareerItem> careerList = new List<CareerItem>();

    public static void SetCareerItem(int careerId, CareerItem item)
    {
        //careerDict[careerId] = item;
        careerList.Add(item);
        if (!careerDict.ContainsKey(careerId))
        {
            careerDict[careerId] = new List<CareerItem>();
        }
        List<CareerItem> items = careerDict[careerId];
        items.Add(item);
    }

    public static int GetCareerCount()
    {
        return careerDict.Count;
    }

    //public static Dictionary<int, List<CareerItem>>.ValueCollection GetAllCareerItem()
    //{
    //    return careerDict.Values;
    //}

    public static CareerItem GetLevelItem(int careerId, int level)
    {
        List<CareerItem> careerItems = careerDict[careerId];
        CareerItem desLevel = null;
        foreach (CareerItem item in careerItems)
        {
            if (item.level == level)
            {
                desLevel = item;
                break;
            }
        }
        return desLevel;
    }

    public static List<CareerItem> GetAllCareerItems()
    {
        List<CareerItem> items = new List<CareerItem>();
        int flagCareerId = 0;
        for (int i = 0; i < careerList.Count; i++)
        {
            if(flagCareerId != careerList[i].careerId)
            {
                items.Add(careerList[i]);
                flagCareerId = careerList[i].careerId;
            }
        }
        return items;
    }

}
