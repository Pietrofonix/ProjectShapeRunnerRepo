using UnityEngine;

public class WallWalkerController : MonoBehaviour
{
    [SerializeField] Transform m_player;
    [SerializeField] float m_speed1;
    [SerializeField] float m_speed2;
    bool m_velocity;

    private void Start()
    {
        m_velocity = false;
    }

    void FixedUpdate ()
    {
        Vector3 target = new Vector3(transform.position.x, transform.position.y, m_player.position.z);

        if (Vector3.Distance(transform.position, target) <= 20 && Vector3.Distance(transform.position, target) >= 10)
        {
            m_velocity = true;

            WalkerMovement();
        }
        else if (Vector3.Distance(transform.position, target) < 10)
        {
            m_velocity = false;

            WalkerMovement();
        }
    }

    void WalkerMovement ()
    {
        Vector3 target = new Vector3(m_player.position.x, transform.position.y, transform.position.z);

        if (m_velocity)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, m_speed1 * Time.fixedDeltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target, m_speed2 * Time.fixedDeltaTime);
        }
    }
}
