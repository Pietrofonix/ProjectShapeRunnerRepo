using System;
using UnityEngine;
using TMPro;

public class StopWatchController : MonoBehaviour
{
    public TextMeshProUGUI UIText;
    float t;

    void Update()
    {

        //"CultureInfo.InvariantCulture" with using System.Globalization to use the . instead of , for fractions of second;
        t += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(t);
        UIText.text = timeSpan.ToString(@"mm\:ss\:fff");
    }
}
