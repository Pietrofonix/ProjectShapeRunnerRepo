using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    enum State
    {
        idle,
        shoot
    }

    State state = State.idle;

    public Transform enemyTargetForward;
    public Transform enemyTargetBackward;
    public Transform player;
    public Transform firePoint;
    public GameObject bullet;
    public LayerMask whatIsPlayer;
    [SerializeField] float sightRange;
    [SerializeField] float attackCooldownTimer;
    bool waitForAttack = false;
    bool m_playerDetected = false;

    void Update()
    {
        m_playerDetected = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        switch (state)
        {
            case State.idle:
                DetectPlayer();
                break;

            case State.shoot:
                foreach (Transform child in player)
                {
                    Vector3 enemyAim;
                    if (PlayerController.isMovingForward && PlayerController.isMoving)
                    {
                        enemyAim = enemyTargetForward.position;
                        enemyAim.y -= 0.5f;
                        transform.LookAt(enemyAim);
                        Debug.Log("Sparo al target davanti");
                    }
                    else if(!PlayerController.isMovingForward && PlayerController.isMoving)
                    {
                        enemyAim = enemyTargetBackward.position;
                        enemyAim.y -= 0.5f;
                        transform.LookAt(enemyAim);
                        Debug.Log("Sparo al target dietro");
                    }     
                    else if(!PlayerController.isMoving)
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
                    Invoke(nameof(AttackCooldown), attackCooldownTimer);
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
        Instantiate(bullet, firePoint.position, transform.rotation);

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
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
