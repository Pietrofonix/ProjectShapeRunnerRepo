using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSphere : MonoBehaviour
{
    PlayerController m_playerController;
    [SerializeField] float m_healthUpAmount;

    void Start()
    {
        m_playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (m_playerController.Health <= (100 - m_healthUpAmount))
            {
                m_playerController.Health += m_healthUpAmount;
                Destroy(gameObject);
            }
        }    
    }
}
