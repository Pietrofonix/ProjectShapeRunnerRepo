using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelsTimeManager : MonoBehaviour
{
    [SerializeField] int m_levelSelection;
    [SerializeField] TextMeshProUGUI m_bestLevelTimeText;
    string m_sceneName;

    void Start()
    {
        m_sceneName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(m_levelSelection));
    }

    void Update()
    {
        if (!PlayerPrefs.HasKey(m_sceneName + "BestTime"))
        {
            m_bestLevelTimeText.text = "";
        }
        else
        {
            m_bestLevelTimeText.text = "Best time: " + PlayerPrefs.GetString(m_sceneName + "BestTime");
        }
    }
}
