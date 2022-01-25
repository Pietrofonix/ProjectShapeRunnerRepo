using UnityEngine;

public class HealthSphere : MonoBehaviour
{
    //PlayerController m_playerController;
    [SerializeField] float m_healthUpAmount;
    Collider m_coll;
    MeshRenderer m_mr;

    void Start()
    {
        //m_playerController = FindObjectOfType<PlayerController>();
        m_coll = GetComponent<Collider>();
        m_mr = GetComponent<MeshRenderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (/*m_playerController.Health*/ GameManager.Instance.PlayerHealth <= (100f - m_healthUpAmount))
            {
                //m_playerController.Health += m_healthUpAmount;
                GameManager.Instance.PlayerHealth += m_healthUpAmount;
                m_coll.enabled = false;
                m_mr.enabled = false;
            }
            else if (GameManager.Instance.PlayerHealth < 100f)
            {
                GameManager.Instance.PlayerHealth = 100f;
                m_coll.enabled = false;
                m_mr.enabled = false;
            }
        }    
    }
}
