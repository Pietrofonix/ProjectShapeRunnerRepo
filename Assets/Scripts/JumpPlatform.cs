using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField] float m_jumpBoost;
    public PlayerController PlayerScript;
    public GameObject Capsule;
    public Rigidbody PlayerRb;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerRb.velocity = Vector3.zero;
            PlayerRb.AddForce(Vector3.up * m_jumpBoost, ForceMode.Impulse);
            //m_playerRb.velocity = new Vector3(0f, m_jumpBoost, 0f);
            PlayerScript.DoubleJumpVar = true;
        }    
    }
}
