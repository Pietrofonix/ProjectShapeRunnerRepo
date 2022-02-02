using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndLevelController : MonoBehaviour
{
    [SerializeField] GameObject m_endMenu;
    [SerializeField] GameObject m_stopWatch;
    [SerializeField] StopWatchController m_stopWatchController;
    [SerializeField] TextMeshProUGUI m_timerScore;
    [SerializeField] TextMeshProUGUI m_bestScore;
    [SerializeField] PauseManager m_pauseMenu;
    [SerializeField] PlayerController m_player;
    [SerializeField] Rigidbody m_playerRb;
    [SerializeField] ShapesWheelController m_shapesWheelController;
    int m_levelLoad;

    void Start()
    {
        m_levelLoad = SceneManager.GetActiveScene().buildIndex;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_endMenu.SetActive(true);
            m_stopWatch.SetActive(false);
            m_player.enabled = false;
            m_pauseMenu.enabled = false;
            m_stopWatchController.enabled = false;
            m_playerRb.velocity = Vector3.zero;
            m_timerScore.text = m_stopWatchController.UIText.text;
            m_bestScore.text = PlayerPrefs.GetFloat("BestScore").ToString();
            m_shapesWheelController.enabled = false;
        }
    }

    public void NextLevel()
    {
        m_levelLoad++;
        SceneManager.LoadScene(m_levelLoad);
    }
}

