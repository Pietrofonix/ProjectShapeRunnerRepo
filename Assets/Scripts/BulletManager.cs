using UnityEngine;

public class BulletManager : MonoBehaviour
{
    Rigidbody m_rb;
    Transform m_playerRef;
    PlayerController m_playerController;
    [SerializeField] float m_bulletSpeed;
    [SerializeField] float m_bulletUpForce;
    private GameObject cylinder;
    //public Image cylinderPerkBar;
    //Vector3 bulletDir;

    /*void Awake()
    {
        m_rb = GetComponent<Rigidbody>(); 
    }*/

    void Start()
    {
        #region Robe
        /*m_rb = GetComponent<Rigidbody>();
        target = GameObject.FindObjectOfType<PlayerController>();
        bulletDir = (target.transform.position - transform.position).normalized * m_bulletSpeed;
        m_rb.velocity = new Vector3(0f, 0f, bulletDir.z);*/
        //m_rb.velocity = (player.position - transform.position) * m_bulletSpeed; 
        #endregion

        m_playerController = FindObjectOfType<PlayerController>();
        cylinder = GameObject.Find("ShapeManager/CylinderPlayer");
        m_rb = GetComponent<Rigidbody>();
        m_playerRef = GameObject.FindObjectOfType<PlayerController>().transform;
        //bulletDir = player.position - transform.position;
        //transform.forward = bulletDir.normalized;
        m_rb.AddForce(transform.forward * m_bulletSpeed, ForceMode.Impulse);
        //m_rb.AddForce(transform.up * m_bulletUpForce, ForceMode.Impulse);
        //m_rb.velocity = bulletDir.normalized * m_bulletSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if(!cylinder || (cylinder && !CylinderPerk.startCylinderPerkBar && CylinderPerk.startCooldownCylinder))
            {
                m_playerController.Health -= 10f;
                Destroy(gameObject);
                //gameObject.SetActive(false);
            }
            else if(cylinder && CylinderPerk.startCylinderPerkBar)
            {
                Destroy(gameObject);
                //gameObject.SetActive(false);
                //cylinderPerkBar.enabled = true;
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
        //gameObject.SetActive(false);
    }
}
