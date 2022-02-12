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
        //PlayerPrefs.DeleteKey(SceneManager.GetActiveScene().name + "MinTime");
        //PlayerPrefs.DeleteKey(SceneManager.GetActiveScene().name + "BestTime");
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
            m_timerScore.text = "Current time: " + m_stopWatchController.TimeScore.text;
            m_shapesWheelController.enabled = false;

            if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "MinTime"))
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "MinTime", m_stopWatchController.TimeScoreToFloat);
                PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "BestTime", m_stopWatchController.TimeScore.text);
                BestTime();
            }
            else if (PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "MinTime") > m_stopWatchController.TimeScoreToFloat)
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "MinTime", m_stopWatchController.TimeScoreToFloat);
                PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "BestTime", m_stopWatchController.TimeScore.text);
                BestTime();
            }
            else
            {
                BestTime();
            }
        }
    }

    public void NextLevel()
    {
        m_levelLoad++;
        SceneManager.LoadScene(m_levelLoad);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.PlayerHealth = 100f;
    }

    void BestTime()
    {
        m_bestScore.text = "Best time: " + PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "BestTime");
    }
}

