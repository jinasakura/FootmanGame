using UnityEngine;

/// <summary>
/// 玩家共同信息(playerId、playerName、detail等)
/// 变成人物共有信息是因为各个类的需求无法统一
/// </summary>
public class PlayerInfo : MonoBehaviour
{
    public int playerId;

    private string _playerName;
    public string playerName
    {
        get { return _playerName; }
        set
        {
            _playerName = value;
            gameObject.name = value;
        }
    }

    public string modelName;

    public int careerId;

    public PlayerDetailInfo detail;

}
