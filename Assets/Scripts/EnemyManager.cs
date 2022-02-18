using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    enum State
    {
        idle,
        shoot
    }

    State state = State.idle;

    [SerializeField] GameObject m_bullet;
    [SerializeField] Transform m_player;
    [SerializeField] LayerMask m_whatIsPlayer;
    [SerializeField] PlayerController m_playerScript;
    [SerializeField] Rigidbody m_playerRb;
    [SerializeField] float m_sightRange;
    [SerializeField] float m_attackCooldownTimer;
    public Transform FirePoint;
    Transform m_enemyTargetForward;
    Transform m_enemyTargetBackward;
    RaycastHit m_enemySight;
    Vector3 m_raycastDir;
    bool waitForAttack = false;
    bool m_playerInRange = false;
    bool m_obstacle = false;

    void Start()
    {
        m_enemyTargetForward = m_player.GetChild(5);
        m_enemyTargetBackward = m_player.GetChild(6);
    }

    void Update()
    {
        //Check if player is in range (inside the sphere)
        m_playerInRange = Physics.CheckSphere(transform.position, m_sightRange, m_whatIsPlayer);

        //Check if player is visible
        m_raycastDir = (m_player.position - transform.position);
        m_obstacle = Physics.Raycast(transform.position, m_raycastDir, out m_enemySight, m_sightRange);

        switch (state)
        {
            case State.idle:
                DetectPlayer();

                if(transform.rotation.eulerAngles != Vector3.zero && !m_playerInRange)
                {
                    Quaternion targetRot = Quaternion.Euler(0f, 0f, 0f);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 2f * Time.deltaTime); 
                }
                break;

            case State.shoot:
                foreach (Transform child in m_player)
                {
                    Vector3 enemyAim;
                    //If the player is moving forward, the enemy shoots behind of the player
                    if (m_playerScript.IsMovingForward && m_playerScript.IsMoving)
                    {
                        enemyAim = m_enemyTargetBackward.position;
                        enemyAim.y -= 0.5f;
                        transform.LookAt(enemyAim);
                    }
                    //If the player is moving forward, the enemy shoots in front of the player
                    else if (!m_playerScript.IsMovingForward && m_playerScript.IsMoving)
                    {
                        enemyAim = m_enemyTargetForward.position;
                        enemyAim.y -= 0.5f;
                        transform.LookAt(enemyAim);
                    }     
                    //If the player is not moving, the enemy shoots directly at the player 
                    else if (!m_playerScript.IsMoving)
                    {
                        if (!child.CompareTag("GroundCheck") && !child.CompareTag("EnemyTarget") && child.gameObject.activeInHierarchy)
                        {
                            enemyAim = child.position;
                            enemyAim.y -= 0.5f;
                            transform.LookAt(enemyAim);
                        }
                    }
                }

                if (!waitForAttack)
                {
                    ShootPlayer();
                    waitForAttack = true;
                    Invoke(nameof(AttackCooldown), m_attackCooldownTimer);
                }
                break;
        }
    }

    void AttackCooldown()
    {
        waitForAttack = false;
    }

    void DetectPlayer()
    {
        if (m_playerInRange && m_enemySight.collider != null)
        {   
            if (m_obstacle && !m_enemySight.collider.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, m_raycastDir, Color.red);
            } 
            else if (m_obstacle && m_enemySight.collider.CompareTag("Player"))
            {
                m_playerRb.WakeUp();
                state = State.shoot;
            }
        }
    }

    void ShootPlayer()
    {
        Instantiate(m_bullet, FirePoint.position, transform.rotation);

        #region Pool method
        /*GameObject bullet = ObjectPool.instance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.SetPositionAndRotation(firePoint.position, transform.rotation);
            bullet.SetActive(true);
        }
        else
            return;*/
        #endregion

        //GameObject newBullet = Instantiate(bullet, firePoint.position, transform.rotation);
        //newBullet.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * m_bulletSpeed, ForceMode.Impulse);
        //newBullet.GetComponent<Rigidbody>().velocity = (player.position - firePoint.position).normalized * m_bulletSpeed;
        if (!m_playerInRange || (m_obstacle && !m_enemySight.collider.CompareTag("Player")))
        {
            state = State.idle;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_sightRange);
    }
}
