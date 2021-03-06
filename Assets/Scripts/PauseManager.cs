using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject PnlPause;
    [SerializeField] PlayerController m_playerController;
    [SerializeField] ShapesWheelController m_shapesWheelController;
    [SerializeField] GameObject m_shapesWheel;
    bool m_isPaused;

    void Start()
    {
        m_isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ChangeStatusMenuPause();
        }
    }

    void ChangeStatusMenuPause()
    {
        m_isPaused = !m_isPaused;
        UpdateGamePause();
    }

    void UpdateGamePause()
    {
        if (m_isPaused)
        {
            Time.timeScale = 0;
            m_playerController.enabled = false;
            m_shapesWheelController.enabled = false;
            m_shapesWheel.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            m_playerController.enabled = true;
            m_shapesWheelController.enabled = true;
        }

        PnlPause.SetActive(m_isPaused);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}