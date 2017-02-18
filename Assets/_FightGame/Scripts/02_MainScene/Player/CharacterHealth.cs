using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public float maxHealth = 100f;               
    public Slider slider;                             
    public Image fillImage;                           
    public Color fullHealthColor = Color.green;       
    public Color zeroHealthColor = Color.red;         
    //public GameObject m_ExplosionPrefab;                


    //private AudioSource m_ExplosionAudio;               
    //private ParticleSystem m_ExplosionParticles;
    [SerializeField]    
    private float currentHp;                      
    //private bool dead;                              


    //private void Awake()
    //{
    //    m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();

    //    m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

    //    m_ExplosionParticles.gameObject.SetActive(false);
    //}


    private void OnEnable()
    {
        currentHp = maxHealth;
        //dead = false;

        SetHealthUI();
    }


    public void TakeDamage(float amount)
    {
        currentHp -= amount;

        SetHealthUI();

        if (currentHp <= 0f)
        {
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
        NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.CharacterDie);
        //dead = true;

        //m_ExplosionParticles.transform.position = transform.position;
        //m_ExplosionParticles.gameObject.SetActive(true);

        //m_ExplosionParticles.Play();

        //m_ExplosionAudio.Play();

        //gameObject.SetActive(false);
    }
}