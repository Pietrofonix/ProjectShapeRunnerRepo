using UnityEngine;

public class SmashWallController : MonoBehaviour
{
    [SerializeField] float m_smashWallSpeedUp;
    [SerializeField] float m_smashWallSpeedDown;
    [SerializeField] float m_smashWallDistance;
    bool m_smashWall;
    float m_smashWallYPosSave;

    void Start()
    {
        m_smashWall = false;
        m_smashWallYPosSave = transform.position.y;
    }

    void FixedUpdate()
    {
        Vector3 smashWallPositiveTarget = new(transform.position.x, m_smashWallYPosSave + m_smashWallDistance, transform.position.z);
        Vector3 smashWallNegativeTarget = new(transform.position.x, m_smashWallYPosSave, transform.position.z);

        if (!m_smashWall)
        {
            transform.position = Vector3.MoveTowards(transform.position, smashWallPositiveTarget, m_smashWallSpeedUp * Time.fixedDeltaTime);

            if (transform.position.y >= m_smashWallYPosSave + m_smashWallDistance)
            {
                m_smashWall = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, smashWallNegativeTarget, m_smashWallSpeedDown * Time.fixedDeltaTime);

            if (transform.position.y <= m_smashWallYPosSave)
            {
                m_smashWall = false;
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && m_smashWall)
        {
            GameManager.Instance.PlayerHealth = 0f;
        }
    }
}