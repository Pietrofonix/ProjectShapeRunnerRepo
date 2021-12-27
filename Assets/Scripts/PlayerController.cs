using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    #region Gravity variables
    [SerializeField] float m_magneticForce;
    [SerializeField] float m_rayRange;
    [SerializeField] GameObject m_vCam1;
    [SerializeField] GameObject m_vCam2;
    public LayerMask platformUp;
    public GameObject sphere;
    bool m_gravityChange = false;
    bool m_isOnTop = false;
    bool m_goUp = false;
    bool m_platformUphit;
    #endregion

    #region BoostSpeed
    bool m_gonnaGoFast = false;
    [SerializeField] float m_boostSpeedTimer;
    [SerializeField] float m_speedMultiplier;
    [SerializeField] float m_jumpBoost;
    float m_jumpBoostSave = 0f;
    float m_boostSpeedTimerSave = 0f;
    #endregion

    #region WallRun variables
    public LayerMask whatIsWall;
    public GameObject cube;
    public float wallRunForce;
    bool m_isWallRight, m_isWallLeft, m_isWallRunning;
    bool m_startWallRun = false;
    bool m_jumpFromWall = false;
    [SerializeField] float m_wallRunJumpMultiplier;
    [SerializeField] float m_yWallRunVelocity;
    #endregion

    #region GroundCheck variables
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool m_isGrounded = true;
    #endregion

    #region Rigidbody variables
    public float gravity;
    private float groundedGravity = 0.05f;
    Rigidbody m_rb;
    Vector3 moveDir;
    #endregion

    #region Player variables
    [SerializeField] float m_playerSpeed;
    [SerializeField] int m_howManyShapes;
    [SerializeField] float m_playerAirSpeed;
    [SerializeField] float m_jumpForce;
    [SerializeField] float m_jumpForceWall;
    [SerializeField] float m_wallJumpUpForce;
    bool m_doubleJump;
    public int selectedShape = 0;
    public float health = 100f;
    public static bool isMovingForward = false;
    public static bool isMoving = false;
    //[SerializeField] float m_playerRunSpeed;
    //[SerializeField] int min = 0, max = 2;
    #endregion

    void Start()
    {
        SelectedShape();
        m_rb = GetComponent<Rigidbody>();
        m_boostSpeedTimerSave = m_boostSpeedTimer;
        m_jumpBoostSave = m_jumpForce;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

        #region Debug
        //Debug.Log("m_gravityChange: " + m_gravityChange);
        //Debug.Log("m_isOnTop: " + m_isOnTop);
        //Debug.Log("StartWallRun: " + m_startWallRun);
        //Debug.Log("Wall Run= " + m_isWallRunning);
        //Debug.Log("Ground= " + m_isGrounded);
        //Debug.Log("Jump From Wall= " + m_jumpFromWall);
        //Debug.Log(m_rb.velocity);
        //Debug.Log("Il salto dal muro è: " + m_jumpFromWall);
        //Debug.Log("Sono a terra: " + m_isGrounded);
        //Debug.Log("Doppio salto: " + m_doubleJump);
        //Debug.Log("Jumps: " + m_jumpsCount);
        //Debug.Log("Si sta muovendo?: " + isMovingForward);
        //Debug.Log("Doppio salto: " + m_doubleJump);
        #endregion

        CheckJump();

        if (!m_isWallRunning && !m_gravityChange)
            ScrollShape();

        CheckGround();

        if (cube.activeInHierarchy)
        {
            CheckForWall();
            JumpFromWall();
        } 
        else if (sphere.activeInHierarchy)
        {
            CheckGravityChange();
        }

        if (!sphere.activeInHierarchy)
            m_gravityChange = false;

        BoostSpeedTimer();
        //RestartScene();

        /*if (m_isWallRunning)
            m_rb.AddForce(Vector3.down * m_downForce, ForceMode.Force);*/
    }

    void FixedUpdate()
    {
        Movement();
    }

    #region Movement
    void Movement()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(xMov, 0f, zMov);
        moveDir.Normalize();

        if (moveDir != Vector3.zero)
        {
            isMoving = true;
            if (zMov > 0f)
                isMovingForward = true;
            else
                isMovingForward = false;
        }      
        else
            isMoving = false;

        if (m_isGrounded && !m_isWallRunning)
        {
            m_jumpFromWall = false;
            moveDir.x *= m_playerSpeed;
            moveDir.z *= m_playerSpeed;
            moveDir.y = m_rb.velocity.y;
            m_rb.AddForce(Vector3.down * groundedGravity, ForceMode.Acceleration);
            m_rb.velocity = moveDir;
            //m_rb.AddForce(moveDir, ForceMode.Force);
            //m_rb.velocity = Vector3.ClampMagnitude(m_rb.velocity, m_playerSpeed);
        }

        #region Different sphere movement

        /*else if (m_isGrounded && !m_isWallRunning && sphere.activeInHierarchy)
        {
            m_jumpFromWall = false;
            m_rb.AddForce(moveDir * 300 * Time.fixedDeltaTime, ForceMode.Acceleration);
            if (moveDir.sqrMagnitude < 0.01f) 
                m_rb.AddForce(-m_rb.velocity * m_playerSpeed, ForceMode.Acceleration);
        }*/
        #endregion

        else if (!m_isGrounded && !m_isWallRunning && !m_jumpFromWall)
        {
            moveDir.x *= m_playerAirSpeed;
            moveDir.z *= m_playerSpeed;
            moveDir.y = m_rb.velocity.y - gravity * Time.deltaTime;
            //m_rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
            m_rb.velocity = moveDir;
        } 
        else if (!m_isGrounded && m_jumpFromWall && !m_isWallRunning)
        {
            m_rb.velocity = new Vector3(m_rb.velocity.x, m_rb.velocity.y, m_rb.velocity.z);
            m_rb.AddForce(Vector3.down * 9.81f, ForceMode.Acceleration);

        }
        else if (!m_isGrounded && !m_jumpFromWall && m_isWallRunning)
        {
            //The player automatically goes forward while running on the wall
            m_rb.velocity = new Vector3(0f, 0f, m_playerSpeed);

            //Let the player go up and down while running on the wall
            if (Input.GetKey(KeyCode.W))
            {
                m_rb.velocity = new Vector3(0f, m_yWallRunVelocity, m_rb.velocity.z);
            }
            if (Input.GetKey(KeyCode.S))
            {
                m_rb.velocity = new Vector3(0f, -m_yWallRunVelocity, m_rb.velocity.z);
            }
        }
    }
    #endregion

    #region GravityChange
    void CheckGravityChange()
    {
        RaycastHit hit;
        m_platformUphit = Physics.Raycast(transform.position, transform.up, out hit, m_rayRange, platformUp);

        if (m_platformUphit || m_isOnTop)
        {
            GravityChange();
            Debug.DrawRay(transform.position, transform.up * m_rayRange, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.up * m_rayRange, Color.red);
            if (m_gravityChange)
            {
                StopGravityPlatform();
            }
        }       
    }

    void GravityChange()
    {
        Debug.Log("GravityChange: " + m_gravityChange);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!m_isOnTop && !m_gravityChange)
            {
                m_goUp = true;
                m_gravityChange = true;
                m_rb.AddForce(transform.up * m_magneticForce, ForceMode.Force);
                gravity *= -1;
                Debug.Log("Vado su");
                m_vCam1.SetActive(false);
                m_vCam2.SetActive(true);
            }
            else if(m_isOnTop)
            {
                m_goUp = false;
                m_gravityChange = false;
                m_rb.AddForce(-transform.up * m_magneticForce, ForceMode.Force);
                gravity *= -1;
                Debug.Log("Vado giù");
                m_vCam1.SetActive(true);
                m_vCam2.SetActive(false);
            }
            else if(m_goUp && !m_isGrounded)
            {
                m_goUp = false;
                m_gravityChange = false;
                m_rb.AddForce(-transform.up * m_magneticForce, ForceMode.Force);
                gravity *= -1;
                Debug.Log("Torno giù dopo essere andato su");
                m_vCam1.SetActive(true);
                m_vCam2.SetActive(false);
            }
        }
    }

    void StopGravityPlatform()
    {
        m_rb.AddForce(-transform.up * m_magneticForce, ForceMode.Force);
        gravity *= -1;
        m_gravityChange = false;
        m_vCam1.SetActive(true);
        m_vCam2.SetActive(false);
    }
    #endregion

    void CheckGround()
    {
        m_isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(m_isGrounded)
        {
            m_vCam1.SetActive(true);
            m_vCam2.SetActive(false);
        }
        else if(m_isOnTop)
        {
            m_vCam1.SetActive(false);
            m_vCam2.SetActive(true);
        }
    }

    #region ChangeShape
    void ScrollShape()
    {
        int previousSelectedShape = selectedShape;

        if (/*Input.GetAxis("Mouse ScrollWheel") > 0f*/ Input.GetMouseButtonDown(0)) // forward
        {
            if (selectedShape >= transform.childCount - (m_howManyShapes - 1))
                selectedShape = 0;
            else
                selectedShape++;
        }

        if (/*Input.GetAxis("Mouse ScrollWheel") < 0f*/ Input.GetMouseButtonDown(1)) // backwards
        {
            if (selectedShape <= 0)
                selectedShape = transform.childCount - (m_howManyShapes - 1);
            else
                selectedShape--;
        }

        if (previousSelectedShape != selectedShape)
            SelectedShape();
    }

    void SelectedShape()
    {
        int i = 0;

        foreach (Transform shape in transform)
        {
            if (i == selectedShape)
            {
                shape.gameObject.SetActive(true);
            } 
            else
            {
                if(!shape.CompareTag("GroundCheck") && !shape.CompareTag("EnemyTarget"))
                    shape.gameObject.SetActive(false);
            }
            
            i++;
        }

        #region Alternative scroll script
        /*i = Mathf.Clamp(i, min, max);
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            i++;
            if (i > max)
            {
                i = max;
            }
            Debug.Log(i);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            i--;
            if (i < min)
            {
                i = min;
            }
            Debug.Log(i);
        }*/
        #endregion
    }
    #endregion

    #region Jump
    void CheckJump()
    {
        if (m_isGrounded)
        {
            Jump();
        }
        else if (!m_isGrounded)
        {
            DoubleJump();
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            m_rb.velocity = new Vector3(m_rb.velocity.x, m_jumpForce, m_rb.velocity.z);
            m_doubleJump = true;
        }
    }
    
    void DoubleJump()
    {
        if (m_doubleJump && !m_isWallRunning && !m_jumpFromWall && Input.GetKeyDown(KeyCode.Space))
        {
            m_rb.velocity = Vector3.zero;
            m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            m_doubleJump = false;
        }
    }
    #endregion

    #region WallRun
    void CheckForWall()
    {
        m_isWallRight = Physics.Raycast(transform.position, transform.right, 1.5f, whatIsWall);
        m_isWallLeft = Physics.Raycast(transform.position, -transform.right, 1.5f, whatIsWall);

        //Leave wall run
        if (!m_isWallRight && !m_isWallLeft)
        {
            StopWallRun();
            m_startWallRun = false;
        }
        else
            StartWallRun();
    }

    void StopWallRun()
    {
        //m_rb.useGravity = true;
        m_isWallRunning = false;
    }

    void StartWallRun()
    {
        if(!m_startWallRun)
        {
            //m_rb.useGravity = false;
            m_startWallRun = true;
            m_jumpFromWall = false;
            m_isWallRunning = true;
            //m_doubleJump = false;

            //Make sure the player sticks to the wall
            if (m_isWallRight)
            {
                m_rb.AddForce(transform.right * wallRunForce, ForceMode.Force);
            }
            else if (m_isWallLeft)
            {
                m_rb.AddForce(-transform.right * wallRunForce, ForceMode.Force);
            }
        }
    }

    void JumpFromWall()
    {
        if (m_isWallRunning && Input.GetKeyDown(KeyCode.Space))
        {
            m_jumpFromWall = true;
            
            if (m_isWallRight)
            {
                m_rb.AddForce(m_wallRunJumpMultiplier * m_jumpForceWall * -transform.right, ForceMode.Force);
            }

            if (m_isWallLeft)
            {
                m_rb.AddForce(m_wallRunJumpMultiplier * m_jumpForceWall * transform.right, ForceMode.Force);
            }

            m_rb.AddForce(transform.up * m_jumpForceWall * m_wallJumpUpForce);
            //m_rb.AddForce(transform.forward * m_jumpForce * 15f);
            m_isWallRunning = false;
        }
    }
    #endregion

    void RestartScene()
    {
        if (transform.position.y <= -30f || health <= 0f)
        { 
            SceneManager.LoadScene(0);
        }
    }

    #region BoostSpeed
    void BoostSpeedTimer()
    {
        if (m_gonnaGoFast)
        {
            m_boostSpeedTimer -= Time.deltaTime;
            if (m_boostSpeedTimer <= 0.01f)
            {
                m_boostSpeedTimer = m_boostSpeedTimerSave;
                m_playerSpeed /= m_speedMultiplier;
                m_jumpForce = m_jumpBoostSave;
                m_gonnaGoFast = false;
            }
        }
    }

    void BoostSpeed()
    {
        if (!m_gonnaGoFast)
        {
            m_playerSpeed *= m_speedMultiplier;
            m_jumpForce = m_jumpBoost;
            m_gonnaGoFast = true;
        }
    }
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedPlatform"))
        {
            BoostSpeed();
        }    
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GravityPlatform"))
        {
            m_isOnTop = true;
            Debug.Log("Ho toccato: " + m_isOnTop);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("GravityPlatform"))
        {
            m_isOnTop = false;
        }
    }
}
