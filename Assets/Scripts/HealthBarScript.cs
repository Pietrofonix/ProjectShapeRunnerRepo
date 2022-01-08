using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private Image healthBar;
    private float m_currentHealth;
    private float m_maxHealth = 100f;
    //PlayerController player;
    //private GameObject cylinder;

    void Start()
    {
        //cylinder = GameObject.Find("ShapeManager/Cylinder");
        //player = FindObjectOfType<PlayerController>();
        healthBar = GetComponent<Image>();
    }

    void Update()
    {
        //currentHealth = player.Health;
        m_currentHealth = GameManager.Instance.PlayerHealth;
        healthBar.fillAmount = m_currentHealth / m_maxHealth;
    }

}
