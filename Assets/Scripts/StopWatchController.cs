using System;
using UnityEngine;
using TMPro;

public class StopWatchController : MonoBehaviour
{
    [SerializeField] PlayerController m_shapeManager;
    [SerializeField] ShapesWheelController m_shapesWheelController;
    [SerializeField] TextMeshProUGUI m_countDownText;
    [HideInInspector] public TextMeshProUGUI BestScore;
    [HideInInspector] public float TimeScoreToFloat;
    public TextMeshProUGUI TimeScore;
    float m_stopwatch;
    float m_countDown = 3f;

    void FixedUpdate()
    {
        if (m_countDown >= 0f)
        {
            m_countDown -= Time.fixedDeltaTime;
            m_countDownText.text = MathF.Ceiling(m_countDown).ToString();
            m_shapesWheelController.ShapesWheelIsActive = false;
            m_shapeManager.enabled = false;
            TimeScore.enabled = false;
        }
        else if (m_countDown <= 0f)
        {
            m_countDownText.enabled = false;
            TimeScore.enabled = true;
            m_shapeManager.enabled = true;
            m_shapesWheelController.ShapesWheelIsActive = true;

            //"CultureInfo.InvariantCulture" with using System.Globalization to use the . instead of , for fractions of second;
            m_stopwatch += Time.fixedDeltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(m_stopwatch);
            TimeScore.text = timeSpan.ToString(@"mm\:ss\:fff");
            TimeScoreToFloat = (float) timeSpan.TotalSeconds;
        }
    }
}
