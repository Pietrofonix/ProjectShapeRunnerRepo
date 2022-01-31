using UnityEngine;
using UnityEngine.UI;

public class MenuShapeScript : MonoBehaviour
{
    public Color hoverColor;
    public Color baseColor;
    public Image background;

    void Start()
    {
        background.color = baseColor;
    }

    public void Select()
    {
        background.color = hoverColor;
    }

    public void Deselect()
    {
        background.color = baseColor;
    }
}
