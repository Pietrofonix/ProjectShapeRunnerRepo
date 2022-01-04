using System.Collections;
using UnityEngine;
using Cinemachine;
using TMPro;

public class PlayerCheckpoint : MonoBehaviour
{
    Vector3 m_spawnPoint;
    [SerializeField] CinemachineVirtualCamera m_vCam1;
    PlayerController m_playerController;
    [SerializeField] TextMeshProUGUI m_checkpointText;

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
            if(m_playerController.Sphere.activeInHierarchy || m_playerController.Cube.activeInHierarchy || m_playerController.Cone.activeInHierarchy)
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

    IEnumerator DisplayText()
    {
        m_checkpointText.enabled = true;
        yield return new WaitForSeconds(1f);
        m_checkpointText.enabled = false;
        yield return null;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Checkpoint"))
        {
            m_spawnPoint = other.transform.position;
            other.gameObject.SetActive(false);
            StartCoroutine(nameof(DisplayText));
        }    
    }
}
