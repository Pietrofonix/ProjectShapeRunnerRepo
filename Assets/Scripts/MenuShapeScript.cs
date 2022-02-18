using UnityEngine;
using UnityEngine.UI;

public class MenuShapeScript : MonoBehaviour
{
    public Color HoverColor;
    public Color BaseColor;
    public Image Background;
    Vector3 m_bgLocalScale;

    void Start()
    {
        Background.color = BaseColor;
        m_bgLocalScale = Background.rectTransform.localScale;
    }

    public void Select()
    {
        Background.color = HoverColor;
        Background.rectTransform.localScale = new Vector3(m_bgLocalScale.x + 0.05f, m_bgLocalScale.y + 0.05f, m_bgLocalScale.z + 0.05f);
    }

    public void Deselect()
    {
        Background.color = BaseColor;
        Background.rectTransform.localScale = m_bgLocalScale;
    }
}
