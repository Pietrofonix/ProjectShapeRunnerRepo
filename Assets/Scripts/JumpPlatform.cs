using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField] float m_jumpBoost;
    public Rigidbody m_player;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            m_player.AddForce(Vector3.up * m_jumpBoost, ForceMode.Impulse);
        }    
    }
}
