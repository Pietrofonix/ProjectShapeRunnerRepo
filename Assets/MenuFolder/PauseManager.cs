using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject PnlPause;
    [SerializeField] PlayerController m_playerController;
    [SerializeField] ShapesWheelController m_shapesWheelController;
    bool m_isPaused;

    void Start()
    {
        m_isPaused = false;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            ChangeStatusMenuPause();
        }

        //Debug.Log(m_isPaused);
    }

    void ChangeStatusMenuPause()
    {
        m_isPaused = !m_isPaused;
        UpdateGamePause();
    }

    void UpdateGamePause()
    {
        if(m_isPaused)
        {
            // Time.timeScale si usa per velocizzare, rallentare o fermare il tempo
            Time.timeScale = 0;
            m_playerController.enabled = false;
            m_shapesWheelController.enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            m_playerController.enabled = true;
            m_shapesWheelController.enabled = true;
        }

        PnlPause.SetActive(m_isPaused);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}