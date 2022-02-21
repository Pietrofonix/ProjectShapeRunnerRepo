using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [SerializeField] float m_elevatorSpeed;
    [SerializeField] float m_elevatorDistance;
    bool m_elevator;
    float m_elevatorYPosSave;

    void Start()
    {
        m_elevator = false;
        m_elevatorYPosSave = transform.position.y;
    }

    void FixedUpdate()
    {
        Vector3 elevatorPositiveTarget = new(transform.position.x, m_elevatorYPosSave + m_elevatorDistance, transform.position.z);
        Vector3 elevatorNegativeTarget = new(transform.position.x, m_elevatorYPosSave, transform.position.z);

        if (!m_elevator)
        {
            transform.position = Vector3.MoveTowards(transform.position, elevatorPositiveTarget, m_elevatorSpeed * Time.fixedDeltaTime);

            if (transform.position.y >= m_elevatorYPosSave + m_elevatorDistance)
            {
                m_elevator = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, elevatorNegativeTarget, m_elevatorSpeed * Time.fixedDeltaTime);

            if (transform.position.y <= m_elevatorYPosSave)
            {
                m_elevator = false;
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
