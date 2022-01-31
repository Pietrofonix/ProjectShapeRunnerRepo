using UnityEngine;

public class MovementPlatform : MonoBehaviour
{
    [SerializeField] float m_platformSpeed;
    [SerializeField] float m_platformDistance;
    bool m_platformController;
    float m_platformXPosSave;

    void Start()
    {
        m_platformController = false;
        m_platformXPosSave = transform.position.x;
    }

    void FixedUpdate()
    {
        Vector3 platformPositiveTarget = new(transform.position.x + 1f, transform.position.y, transform.position.z);
        Vector3 platformNegativeTarget = new(transform.position.x - 1f, transform.position.y, transform.position.z);

        if (!m_platformController)
        {
            transform.position = Vector3.MoveTowards(transform.position, platformPositiveTarget, m_platformSpeed * Time.fixedDeltaTime);

            if (transform.position.x >= m_platformXPosSave + m_platformDistance)
            {
                m_platformController = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, platformNegativeTarget, m_platformSpeed * Time.fixedDeltaTime);

            if (transform.position.x <= m_platformXPosSave - m_platformDistance)
            {
                m_platformController = false;
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
