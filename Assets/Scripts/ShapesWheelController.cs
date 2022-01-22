using UnityEngine;
using UnityEngine.UI;

public class ShapesWheelController : MonoBehaviour
{
    //public Animator m_anim;
    bool m_shapeWheelSelected = false;
    public GameObject ShapeWheel;
    //public Image SelectedShape;
    public static int ShapeID;
    public Transform Player;

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            //m_shapeWheelSelected = !m_shapeWheelSelected;
            Time.timeScale = 0.3f;
            m_shapeWheelSelected = true;
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            m_shapeWheelSelected = false;
            Time.timeScale = 1f;
        }

        ShapeWheel.SetActive(m_shapeWheelSelected);

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
                foreach(Transform shape in Player)
                {
                    if(!shape.CompareTag("CylinderPlayer") && !shape.CompareTag("GroundCheck") && !shape.CompareTag("EnemyTarget"))
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
                foreach (Transform shape in Player)
                {
                    if (!shape.CompareTag("CapsulePlayer") && !shape.CompareTag("GroundCheck") && !shape.CompareTag("EnemyTarget"))
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
                foreach (Transform shape in Player)
                {
                    if (!shape.CompareTag("ConePlayer") && !shape.CompareTag("GroundCheck") && !shape.CompareTag("EnemyTarget"))
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
                foreach (Transform shape in Player)
                {
                    if (!shape.CompareTag("CubePlayer") && !shape.CompareTag("GroundCheck") && !shape.CompareTag("EnemyTarget"))
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
        ShapeWheel.SetActive(false);
    }
}
