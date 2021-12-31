using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    //Slider compare solo se si usa la libreria: UnityEngine.UI
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
    }

    private void Load()
    {
        m_volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Safe()
    {
        PlayerPrefs.SetFloat("musicVolume", m_volumeSlider.value);
    }
}
