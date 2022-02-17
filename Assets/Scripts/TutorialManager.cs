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
            m_tutorialText.text = "Press W A S D to move";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "JumpColl":
                m_tutorialText.text = "PRESS SPACEBAR TO JUMP \n\nCAPSULE CAN DOUBLE JUMP";
                break;

            case "PlatColl1":
                m_tutorialText.text = "Blue carpet boosts your speed\n\nRed carpet slows you DOWN";
                break;

            case "PlatColl2":
                m_tutorialText.text = "MOVING ON A SPEED CARPET WHILE SLOWED WILL RETURN YOU TO NORMAL SPEED AND VICEVERSA";
                break;

            case "WheelColl":
                m_tutorialText.text = "HOLDING DOWN THE RIGHT MOUSE BUTTON WILL OPEN THE SHAPE WHEEL\n\nCYLINDER IS BULLET RESISTENT FOR A LIMITED TIME";
                break;

            case "HPColl":
                GameManager.Instance.PlayerHealth -= 10f;
                m_tutorialText.text = "WATCH OUT FOR HEALTH SPHERES";
                break;

            case "WallRunColl1":
                m_tutorialText.text = "CUBE CAN ATTACH TO NEARBY WALLS\n\nMOVING PAST A WALL WILL LAUNCH YOU FORWARD";
                break;

            case "WallRunColl2":
                m_tutorialText.text = "WHILE WALLRUNNING Q / E WILL MOVE YOU UP / DOWN\n\nSPACEBAR WILL LAUNCH YOU LEFT / RIGHT \n\nAlways check your height before jumping!";
                break;

            case "ConeColl1":
                m_tutorialText.text = "CONE CAN INVERT GRAVITY \n\nPRESS E WHILE UNDER A GRAVITY PLATFORM \n\n TRY DOUBLE JUMPING";
                break;

            case "SphereColl":
                m_tutorialText.text = "SPHERE INTERACTS WITH GHOST OBJECTS \n\nPRESS E TO MAKE GHOST OBJECTS TANGIBLE BUT SOME OBJECTS WILL BECOME GHOST";
                break;

            case "EndTutorialColl":
                m_tutorialText.text = "GOOD JOB! NOW GO ON WITH THE NEXT LEVELS AND CHALLENGE YOUR FRIENDS TO BEAT YOUR RECORDS!";
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

            case "HPColl":
                m_tutorialText.text = "";
                break;

            case "WallRunColl1":
                m_tutorialText.text = "";
                break;

            case "WallRunColl2":
                m_tutorialText.text = "";
                break;

            case "ConeColl1":
                m_tutorialText.text = "";
                break;

            case "ConeColl2":
                m_tutorialText.text = "";
                break;

            case "JumpPlatColl":
                m_tutorialText.text = "";
                break;

            case "SphereColl":
                m_tutorialText.text = "";
                break;

            case "ObstacleColl":
                m_tutorialText.text = "";
                break;

            case "CheckpointColl":
                m_tutorialText.text = "";
                break;

            case "EndTutorialColl":
                m_tutorialText.text = "";
                break;
        }
    }
}
