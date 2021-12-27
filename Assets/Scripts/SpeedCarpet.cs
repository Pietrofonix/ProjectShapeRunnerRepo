using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCarpet : MonoBehaviour
{
    [SerializeField] float m_speedMultiplier;
    [SerializeField] float m_jumpBoost;
    [SerializeField] float m_boostSpeedTimer;
    public PlayerController playerController;
    bool m_gonnaGoFast = false;
    Collider m_coll;

    void Start()
    {
        m_coll = GetComponent<Collider>();
    }

    void Update()
    {
        /*Debug.Log(playerController.jumpForce);
        //Debug.Log(m_gonnaGoFast);
        SpeedBoost();*/
    }

/*    void SpeedBoost()
    {
        if(m_gonnaGoFast)
        {
            m_boostSpeedTimer -= Time.deltaTime;
            if(m_boostSpeedTimer <= 0.01f)
            {
                playerController.playerSpeed /= m_speedMultiplier;
                playerController.jumpForce = 20f;
                m_boostSpeedTimer = 3f;
                m_coll.enabled = true;
                m_gonnaGoFast = false;
            }
            else
            {
                Debug.Log(m_boostSpeedTimer);
                playerController.playerSpeed *= m_speedMultiplier;
                playerController.jumpForce = m_jumpBoost;
                m_coll.enabled = false;
            }
        }
    }*/

    /*IEnumerator SpeedBoost()
    {
        playerController.playerSpeed *= m_speedMultiplier;
        m_coll.enabled = false;
        //m_jumpForce *= m_speedMultiplier;
        yield return new WaitForSeconds(m_boostSpeedTimer);
        playerController.playerSpeed /= m_speedMultiplier;
        m_coll.enabled = true;
        m_gonnaGoFast = false;
        yield break;
    }*/

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player") && !m_gonnaGoFast)
        {
            m_gonnaGoFast = true;
            //Debug.Log("Il player è entrato");
            //StartCoroutine(nameof(SpeedBoost));
        }
    }
}
