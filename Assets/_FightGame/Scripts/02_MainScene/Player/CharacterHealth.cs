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
    }


    public void TakeDamage(float amount)
    {
        if (currentHp - amount > 0)
        {
            currentHp -= amount;

            SetHealthUI();

            NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.TakeDamage, playerId);
        }
        else
        {
            currentHp = 0;
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        slider.value = currentHp;
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHp / maxHealth);
    }


    private void OnDeath()
    {
        NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.CharacterDie, playerId);
    }
}