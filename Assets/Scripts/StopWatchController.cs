using UnityEngine;
using TMPro;

public class StopWatchController : MonoBehaviour
{
    public TextMeshProUGUI UIText;

    void Update()
    {
        UIText.text = Time.time.ToString("00:00.000");
    }
}
