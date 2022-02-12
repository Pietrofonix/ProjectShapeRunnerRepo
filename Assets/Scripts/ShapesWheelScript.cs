using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapesWheelScript : MonoBehaviour
{
    public Vector2 normalisedMousePosition;
    public float currentAngle;
    public int selection;
    int previousSelection;
    //float m_shapeWheelWidth;
    //float m_shapeWheelHeight;

    [SerializeField] int m_numberOfShapes;
    [SerializeField] GameObject[] m_shapes;

    MenuShapeScript menuCurrentShape;
    MenuShapeScript menuPreviousShape;

    void Update()
    {
        //Debug.Log(previousSelection);

        /*m_shapeWheelWidth = m_shapeWheel.rectTransform.rect.width;
        m_shapeWheelHeight = m_shapeWheel.rectTransform.rect.height;
        normalisedMousePosition = new(Input.mousePosition.x - m_shapeWheelWidth / 2, Input.mousePosition.y - m_shapeWheelHeight / 2);*/
        normalisedMousePosition = new(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
        currentAngle = Mathf.Atan2(normalisedMousePosition.y, normalisedMousePosition.x) * Mathf.Rad2Deg;

        currentAngle = (currentAngle + 360) % 360;
        selection = (int)currentAngle/(360/m_numberOfShapes);

        if (selection != previousSelection)
        {
            menuPreviousShape = m_shapes[previousSelection].GetComponent<MenuShapeScript>();
            menuPreviousShape.Deselect();
            previousSelection = selection;

            menuCurrentShape = m_shapes[selection].GetComponent<MenuShapeScript>();
            menuCurrentShape.Select();
        }
    }  
}
