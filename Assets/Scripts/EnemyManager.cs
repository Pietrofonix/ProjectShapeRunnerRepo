using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    enum State
    {
        idle,
        shoot
    }

    State state = State.idle;

    //public Transform EnemyTargetForward;
    //public Transform EnemyTargetBackward;
    public PlayerController PlayerScript;
    public Transform Player;
    public Transform FirePoint;
    public GameObject Bullet;
    public LayerMask WhatIsPlayer;
    [SerializeField] float m_sightRange;
    [SerializeField] float m_attackCooldownTimer;
    Transform m_enemyTargetForward;
    Transform m_enemyTargetBackward;
    RaycastHit m_enemySight;
    Vector3 m_raycastDir;
    bool waitForAttack = false;
    bool m_playerInRange = false;
    bool m_obstacle = false;

    void Start()
    {
        m_enemyTargetForward = Player.GetChild(5);
        m_enemyTargetBackward = Player.GetChild(6);
    }

    void Update()
    {
        //Check if player is in range (inside the sphere)
        m_playerInRange = Physics.CheckSphere(transform.position, m_sightRange, WhatIsPlayer);

        //Check if player is visible
        m_raycastDir = (Player.position - transform.position);
        m_obstacle = Physics.Raycast(transform.position, m_raycastDir, out m_enemySight, m_sightRange);

        if (m_enemySight.collider != null)
        {
            Debug.Log(m_enemySight.collider.name);
        }

        switch (state)
        {
            case State.idle:
                DetectPlayer();
                break;

            case State.shoot:
                foreach (Transform child in Player)
                {
                    Vector3 enemyAim;
                    //If the player is moving forward, the enemy shoots behind of the player
                    if (PlayerScript.IsMovingForward && PlayerScript.IsMoving)
                    {
                        //enemyAim = EnemyTargetForward.position;
                        enemyAim = m_enemyTargetBackward.position;
                        enemyAim.y -= 0.5f;
                        transform.LookAt(enemyAim);
                        //Debug.Log("Sparo al target davanti");
                    }
                    //If the player is moving forward, the enemy shoots in front of the player
                    else if(!PlayerScript.IsMovingForward && PlayerScript.IsMoving)
                    {
                        //enemyAim = EnemyTargetBackward.position;
                        enemyAim = m_enemyTargetForward.position;
                        enemyAim.y -= 0.5f;
                        transform.LookAt(enemyAim);
                        //Debug.Log("Sparo al target dietro");
                    }     
                    //If the player is not moving, the enemy shoots directly at the player 
                    else if(!PlayerScript.IsMoving)
                    {
                        if (!child.CompareTag("GroundCheck") && !child.CompareTag("EnemyTarget") && child.gameObject.activeInHierarchy)
                        {
                            enemyAim = child.position;
                            enemyAim.y -= 0.5f;
                            transform.LookAt(enemyAim);
                            //Debug.Log("Sparo al player");
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
        if (m_playerInRange)
        {   
            if (m_obstacle && !m_enemySight.collider.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, m_raycastDir, Color.red);
                Debug.Log("Non vedo il giocatore");
            } 
            else if (m_obstacle && m_enemySight.collider.CompareTag("Player"))
            {
                state = State.shoot;
                Debug.Log("Vedo il giocatore");
            }
        }
    }

    void ShootPlayer()
    {
        Instantiate(Bullet, FirePoint.position, transform.rotation);

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
            Debug.Log("Non vedo il giocatore");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_sightRange);
    }
}
