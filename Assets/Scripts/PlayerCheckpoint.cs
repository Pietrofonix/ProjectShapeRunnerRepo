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
    //PillarScript[] m_pillars;
    //HealthSphere[] m_healthSpheres;
    GameObject[] m_pillars;
    GameObject[] m_healthSpheres;
    float m_currentPlayerHealth;

    void Start()
    {
        m_spawnPoint = transform.position;
        m_playerController = GetComponent<PlayerController>();
        //m_pillars = FindObjectsOfType<PillarScript>();
        //m_healthSpheres = FindObjectsOfType<HealthSphere>();
        m_pillars = GameObject.FindGameObjectsWithTag("Pillar");
        m_healthSpheres = GameObject.FindGameObjectsWithTag("HealthSphere");
    }

    void Update()
    {
        if (transform.position.y <= -20f || GameManager.Instance.PlayerHealth <= 0f)
        {
            m_vCam1.enabled = false;
            if (m_playerController.Sphere.activeInHierarchy || m_playerController.Cube.activeInHierarchy || m_playerController.Cone.activeInHierarchy)
            {
                m_spawnPoint.y += 0.5f;
            }
            transform.position = m_spawnPoint;
            GameManager.Instance.PlayerHealth = m_currentPlayerHealth;

            ResetPillars();
            ResetHealthSphere();
            m_playerController.NormalSpeed();

            Invoke(nameof(VCamManager), 0.1f);
        }
    }

    void ResetPillars()
    {
        foreach (GameObject pillar in m_pillars)
        {
            if (pillar.transform.position.z > transform.position.z)
            {
                pillar.GetComponent<Animator>().Rebind();
                pillar.GetComponent<Animator>().Update(0f);
                pillar.GetComponent<Animator>().enabled = false;
            }
        }
    }

    void ResetHealthSphere()
    {
        foreach (GameObject healthSphere in m_healthSpheres)
        {
            if (healthSphere.transform.position.z > transform.position.z)
            {
                healthSphere.GetComponent<Collider>().enabled = true;
                healthSphere.GetComponent<MeshRenderer>().enabled = true;
            }
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
        if (other.CompareTag("Checkpoint"))
        {
            m_spawnPoint = other.transform.position;
            m_currentPlayerHealth = GameManager.Instance.PlayerHealth;
            other.gameObject.SetActive(false);
            StartCoroutine(nameof(DisplayText));
        }    
    }
}
