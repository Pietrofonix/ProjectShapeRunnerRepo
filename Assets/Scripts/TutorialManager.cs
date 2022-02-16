using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_tutorialText;
    float m_timerText = 3f;

    void Update()
    {
        m_timerText -= Time.deltaTime;

        if (m_timerText <= 0f)
        {
            m_timerText = 0f;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("WASDColl") && m_timerText <= 0f)
        {
            m_tutorialText.text = "Press WASD to move";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "JumpColl":
                m_tutorialText.text = "Press SPACE to jump, press SPACE in mid air to double jump";
                break;

            case "PlatColl1":
                m_tutorialText.text = "Blue carpet boosts your speed, Red carpet slows your speed";
                break;

            case "PlatColl2":
                m_tutorialText.text = "Taking the blue carpet while slowed or the red carpet while boosted, return to normal speed";
                break;

            case "WheelColl":
                m_tutorialText.text = "Hold the right mouse button to open the shape wheel. Select the cylinder shape and release it";
                break;

            case "CylinderColl":
                m_tutorialText.text = "Cylinder can block damage until the shield is active. When the shield bar is over, it needs some time to reload";
                break;

            case "HPColl":
                GameManager.Instance.PlayerHealth -= 10f;
                m_tutorialText.text = "Health Sphere recover a bit of health";
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "WASDColl":
                m_tutorialText.text = "";
                break;

            case "JumpColl":
                m_tutorialText.text = "";
                break;

            case "PlatColl1":
                m_tutorialText.text = "";
                break;

            case "PlatColl2":
                m_tutorialText.text = "";
                break;

            case "WheelColl":
                m_tutorialText.text = "";
                break;

            case "CylinderColl":
                m_tutorialText.text = "";
                break;

            case "HPColl":
                m_tutorialText.text = "";
                break;
        }
    }
}
