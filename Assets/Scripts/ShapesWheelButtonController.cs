using UnityEngine;
using TMPro;

public class ShapesWheelButtonController : MonoBehaviour
{
    public int ID;
    public string ShapeName;
    public TextMeshProUGUI ShapeText;
    //public Image SelectedShape;
    public Sprite Icon;
    Animator m_anim;
    bool m_selected = false;

    void Start()
    {
        m_anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (m_selected)
        {
            //SelectedShape.sprite = Icon;
            ShapeText.text = ShapeName;
        }
    }

    public void Selected()
    {
        m_selected = true;
        //ShapesWheelController.ShapeID = ID;
    }

    public void Deselected()
    {
        m_selected=false;
        //ShapesWheelController.ShapeID = 0;
    }

    public void HoverEnter()
    {
        m_anim.SetBool("Hover", true);
        ShapeText.text = ShapeName;
    }

    public void HoverExit()
    {
        m_anim.SetBool("Hover", false);
        ShapeText.text = "";
    }
}
