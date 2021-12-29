using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCheckpoint : MonoBehaviour
{
    Vector3 m_spawnPoint;
    [SerializeField] CinemachineVirtualCamera m_vCam1;
    PlayerController m_playerController;

    void Start()
    {
        m_spawnPoint = transform.position;
        m_playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if(transform.position.y <= -20f)
        {
            m_vCam1.enabled = false;
            if(m_playerController.Sphere.activeInHierarchy || m_playerController.Cube.activeInHierarchy)
            {
                m_spawnPoint.y += 0.5f;
            }
            transform.position = m_spawnPoint;
            Invoke(nameof(VCamManager), 0.1f);
        }
    }

    void VCamManager()
    {
        m_vCam1.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Checkpoint"))
        {
            m_spawnPoint = other.transform.position;
        }    
    }
}
