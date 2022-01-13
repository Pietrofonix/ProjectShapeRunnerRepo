using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelController : MonoBehaviour
{
    int m_levelLoad;

    void Start()
    {
        m_levelLoad = SceneManager.GetActiveScene().buildIndex;
    }

    //OnTriggerEnter serve per controllare se un oggetto con un "tag"
    //entra in contatto col collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_levelLoad++;

            SceneManager.LoadScene(m_levelLoad);
        }
    }
}

