using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public float maxHealth = 100f;               
    public Slider slider;                             
    public Image fillImage;                           
    public Color fullHealthColor = Color.green;       
    public Color zeroHealthColor = Color.red;
    public int playerId;         


    [SerializeField] 
    private float currentHp;
                        


    private void OnEnable()
    {
        currentHp = maxHealth;

        SetHealthUI();

        NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.PlayerCameraChange);
    }


    public void TakeDamage(float amount)
    {
        if (currentHp - amount > 0)
        {
            currentHp -= amount;
            NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.TakeDamageNotice, playerId);
        }
        else
        {
            currentHp = 0;
            NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.CharacterDie, playerId);
            //OnDeath();
        }
        SetHealthUI();
    }


    private void SetHealthUI()
    {
        slider.value = currentHp;
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHp / maxHealth);
    }

    private void PlayerCameraChange(NotificationCenter.Notification info)
    {
        Quaternion q = (Quaternion)info.data;
        slider.transform.rotation = Quaternion.Inverse(q);
    }

    //private void OnDeath()
    //{
    //    NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.CharacterDie, playerId);
    //}
}