using UnityEngine;
using UnityEngine.UI;

public class ShapesWheelController : MonoBehaviour
{
    //public Animator m_anim;
    //public Image SelectedShape;
    [SerializeField] GameObject m_shapesWheel;
    [SerializeField] Transform m_player;
    [SerializeField] float m_slowMotionTime;
    public bool ActivateWheel;
    public static int ShapeID;
    bool m_shapeWheelSelected = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab) && ActivateWheel)
        {
            Time.timeScale = m_slowMotionTime;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            m_shapeWheelSelected = true;
        }
        else
        {
            m_shapeWheelSelected = false;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }

        m_shapesWheel.SetActive(m_shapeWheelSelected);

        /*if (m_shapeWheelSelected)
        {
            //m_anim.SetBool("OpenShapeWheel", true);;
        }
        else
        {
            //m_anim.SetBool("OpenShapeWheel", false);
        }*/

        switch(ShapeID)
        {
            case 0:
                //Debug.Log("Nothing Selected");
                break;
            case 1:
                //Debug.Log("Cylinder active");
                foreach(Transform shape in m_player)
                {
                    if(/*!shape.CompareTag("CylinderPlayer") && */!shape.GetSiblingIndex().Equals(3) && !shape.CompareTag("GroundCheck") && !shape.CompareTag("EnemyTarget"))
                    {
                        shape.gameObject.SetActive(false);
                    }
                    else
                    {
                        shape.gameObject.SetActive(true);
                    }
                }
                break;
            case 2:
                //Debug.Log("Capsule active");
                foreach (Transform shape in m_player)
                {
                    if (/*!shape.CompareTag("CapsulePlayer") &&*/!shape.GetSiblingIndex().Equals(0) && !shape.CompareTag("GroundCheck") && !shape.CompareTag("EnemyTarget"))
                    {
                        shape.gameObject.SetActive(false);
                    }
                    else
                    {
                        shape.gameObject.SetActive(true);
                    }
                }
                break;
            case 3:
                //Debug.Log("Cone active");
                foreach (Transform shape in m_player)
                {
                    if (/*!shape.CompareTag("ConePlayer") &&*/!shape.GetSiblingIndex().Equals(2) && !shape.CompareTag("GroundCheck") && !shape.CompareTag("EnemyTarget"))
                    {
                        shape.gameObject.SetActive(false);
                    }
                    else
                    {
                        shape.gameObject.SetActive(true);
                    }
                }
                break;
            case 4:
                //Debug.Log("Cube active");
                foreach (Transform shape in m_player)
                {
                    if (/*!shape.CompareTag("CubePlayer") &&*/!shape.GetSiblingIndex().Equals(1) && !shape.CompareTag("GroundCheck") && !shape.CompareTag("EnemyTarget"))
                    {
                        shape.gameObject.SetActive(false);
                    }
                    else
                    {
                        shape.gameObject.SetActive(true);
                    }
                }
                break;
        }
    }

    public void CloseWheel()
    {
        m_shapesWheel.SetActive(false);
    }
}
