using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSphere : MonoBehaviour
{
    //PlayerController m_playerController;
    [SerializeField] float m_healthUpAmount;
    private float m_playerHealth;

    void Start()
    {
        //m_playerController = FindObjectOfType<PlayerController>();
        m_playerHealth = GameManager.Instance.PlayerHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (/*m_playerController.Health*/ m_playerHealth <= (100 - m_healthUpAmount))
            {
                //m_playerController.Health += m_healthUpAmount;
                m_playerHealth += m_healthUpAmount;
                Destroy(gameObject);
            }
        }    
    }
}
