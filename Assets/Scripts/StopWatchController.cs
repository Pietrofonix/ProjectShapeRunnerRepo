using System;
using UnityEngine;
using TMPro;

public class StopWatchController : MonoBehaviour
{
    public TextMeshProUGUI UIText;
    float t;
    [HideInInspector] public float BestScore;

    void Start()
    {
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetFloat("BestScore", t);
            Load();
        }    
        else
        {
            Load();
        }
    }

    void Update()
    {
        //"CultureInfo.InvariantCulture" with using System.Globalization to use the . instead of , for fractions of second;
        t += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(t);
        UIText.text = timeSpan.ToString(@"mm\:ss\:fff");
        BestScore = (float) timeSpan.TotalSeconds;
        if (t <= BestScore)
        {
            BestScore = t;
            Save();
        }
    }
    void Load()
    {
        BestScore = PlayerPrefs.GetFloat("BestScore");    
    }

    void Save()
    {
        PlayerPrefs.SetFloat("BestScore", BestScore);
    }

}
