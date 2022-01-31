using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesWheelScript : MonoBehaviour
{
    public Vector2 normalisedMousePosition;
    public float currentAngle;
    public int selection;
    int previousSelection;

    [SerializeField] int m_numberOfShapes;
    [SerializeField] GameObject[] m_shapes;

    MenuShapeScript menuCurrentShape;
    MenuShapeScript menuPreviousShape;

    void Update()
    {
        //Debug.Log(previousSelection);

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
