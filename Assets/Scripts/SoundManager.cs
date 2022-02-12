using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider m_volumeSlider; 

    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = m_volumeSlider.value;
        Save();
    }

    void Load()
    {
        m_volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", m_volumeSlider.value);
    }
}
