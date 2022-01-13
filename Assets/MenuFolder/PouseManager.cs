using UnityEngine;
using UnityEngine.SceneManagement;

public class PouseManager : MonoBehaviour
{
    public GameObject PnlPause;
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
        }
        else
        {
            Time.timeScale = 1;
        }

        PnlPause.SetActive(m_isPaused);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}