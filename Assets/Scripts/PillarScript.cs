using UnityEngine;

public class PillarScript : MonoBehaviour
{
    public Transform Player;
    Animator m_anim;

    void Start()
    {
        m_anim = GetComponent<Animator>(); 
    }

    void Update()
    {
        float distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= 10.0f)
            m_anim.enabled = true;
    }
}
