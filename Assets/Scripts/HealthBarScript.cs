using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    Image healthBar;
    float m_currentHealth;
    float m_maxHealth = 100f;

    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    void Update()
    {
        m_currentHealth = GameManager.Instance.PlayerHealth;
        healthBar.fillAmount = m_currentHealth / m_maxHealth;
    }

}
