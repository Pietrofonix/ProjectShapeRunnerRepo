using UnityEngine;
using UnityEngine.UI;

public class MenuShapeScript : MonoBehaviour
{
    public Color hoverColor;
    public Color baseColor;
    public Image background;
    Vector3 m_bgLocalScale;

    void Start()
    {
        background.color = baseColor;
        m_bgLocalScale = background.rectTransform.localScale;
    }

    public void Select()
    {
        background.color = hoverColor;
        background.rectTransform.localScale = new Vector3(m_bgLocalScale.x + 0.05f, m_bgLocalScale.y + 0.05f, m_bgLocalScale.z + 0.05f);
    }

    public void Deselect()
    {
        background.color = baseColor;
        background.rectTransform.localScale = m_bgLocalScale;
    }
}
