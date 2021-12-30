using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    enum State
    {
        idle,
        shoot
    }

    State state = State.idle;

    public PlayerController PlayerScript;
    //public Transform EnemyTargetForward;
    //public Transform EnemyTargetBackward;
    public Transform Player;
    public Transform FirePoint;
    public GameObject Bullet;
    public LayerMask WhatIsPlayer;
    [SerializeField] float m_sightRange;
    [SerializeField] float m_attackCooldownTimer;
    Transform m_enemyTargetForward;
    Transform m_enemyTargetBackward;
    bool waitForAttack = false;
    bool m_playerDetected = false;

    void Start()
    {
        m_enemyTargetForward = Player.GetChild(5);
        m_enemyTargetBackward = Player.GetChild(6);
    }

    void Update()
    {
        m_playerDetected = Physics.CheckSphere(transform.position, m_sightRange, WhatIsPlayer);

        switch (state)
        {
            case State.idle:
                DetectPlayer();
                break;

            case State.shoot:
                foreach (Transform child in Player)
                {
                    Vector3 enemyAim;
                    if (PlayerScript.IsMovingForward && PlayerScript.IsMoving)
                    {
                        //enemyAim = EnemyTargetForward.position;
                        enemyAim = m_enemyTargetForward.position;
                        enemyAim.y -= 0.5f;
                        transform.LookAt(enemyAim);
                        Debug.Log("Sparo al target davanti");
                    }
                    else if(!PlayerScript.IsMovingForward && PlayerScript.IsMoving)
                    {
                        //enemyAim = EnemyTargetBackward.position;
                        enemyAim = m_enemyTargetBackward.position;
                        enemyAim.y -= 0.5f;
                        transform.LookAt(enemyAim);
                        Debug.Log("Sparo al target dietro");
                    }     
                    else if(!PlayerScript.IsMoving)
                    {
                        if (!child.CompareTag("GroundCheck") && !child.CompareTag("EnemyTarget") && child.gameObject.activeInHierarchy)
                        {
                            enemyAim = child.position;
                            enemyAim.y -= 0.5f;
                            transform.LookAt(enemyAim);
                            Debug.Log("Sparo al player");
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
        if (m_playerDetected)
            state = State.shoot;       
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
        if (!m_playerDetected)
            state = State.idle;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_sightRange);
    }
}
