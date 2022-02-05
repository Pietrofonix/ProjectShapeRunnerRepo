using System;
using UnityEngine;
using TMPro;

public class StopWatchController : MonoBehaviour
{
    public TextMeshProUGUI TimeScore;
    public TextMeshProUGUI BestScore;
    public float TimeScoreToFloat;
    float t;

    void Update()
    {
        //"CultureInfo.InvariantCulture" with using System.Globalization to use the . instead of , for fractions of second;
        t += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(t);
        TimeScore.text = timeSpan.ToString(@"mm\:ss\:fff");
        TimeScoreToFloat = (float) timeSpan.TotalSeconds;
        //Debug.Log(TimeScoreToFloat);
    }
}
