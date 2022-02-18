using UnityEngine;

public class BulletManager : MonoBehaviour
{
    Rigidbody m_rb;
    GameObject cylinder;
    GameObject m_playerController;
    [SerializeField] float m_bulletSpeed;
    [SerializeField] float m_bulletUpForce;
    [SerializeField] EnemyManager m_enemyManager;

    void Start()
    {
        #region Robe
        /*m_rb = GetComponent<Rigidbody>();
        target = GameObject.FindObjectOfType<PlayerController>();
        bulletDir = (target.transform.position - transform.position).normalized * m_bulletSpeed;
        m_rb.velocity = new Vector3(0f, 0f, bulletDir.z);*/
        //m_rb.velocity = (player.position - transform.position) * m_bulletSpeed; 
        #endregion

        //m_playerController = FindObjectOfType<PlayerController>();
        cylinder = GameObject.Find("ShapeManager/CylinderPlayer");
        m_rb = GetComponent<Rigidbody>();
        m_playerController = GameObject.FindGameObjectWithTag("Player");
        //m_playerRef = GameObject.FindObjectOfType<PlayerController>().transform;
        //bulletDir = player.position - transform.position;
        //transform.forward = bulletDir.normalized;
        m_rb.AddForce(transform.forward * m_bulletSpeed, ForceMode.Impulse);
        //m_rb.AddForce(transform.up * m_bulletUpForce, ForceMode.Impulse);
        //m_rb.velocity = bulletDir.normalized * m_bulletSpeed;
    }

    void Update()
    {
        Destroy(gameObject, 2f);

        if (m_playerController.transform.position.y <= -20f || GameManager.Instance.PlayerHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        if (collision.transform.CompareTag("Player"))
        {
            if(!cylinder || (cylinder && !CylinderPerk.startCylinderPerkBar && CylinderPerk.startCooldownCylinder))
            {
                GameManager.Instance.PlayerHealth -= 10f;
                Destroy(gameObject);
            }
            else if(cylinder && CylinderPerk.startCylinderPerkBar)
            {
                Destroy(gameObject);
            }
            else if(cylinder && !CylinderPerk.startCylinderPerkBar && !CylinderPerk.startCooldownCylinder)
            {
                CylinderPerk.startCylinderPerkBar = true;
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
