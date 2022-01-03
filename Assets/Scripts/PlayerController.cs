using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [Header("GravityChange variables")]
    #region Gravity variables
    [SerializeField] float m_magneticForce;
    [SerializeField] float m_rayUpRange;
    [SerializeField] GameObject m_vCam1;
    [SerializeField] GameObject m_vCam2;
    RaycastHit m_hitGravityPlatform;
    public Animator ConeAnim;
    public LayerMask PlatformUp;
    public GameObject Sphere;
    public GameObject Cone;
    bool m_gravityChange = false;
    bool m_isOnTop = false;
    bool m_goUp = false;
    bool m_platformUpHit;
    #endregion
    [Space(2)]

    [Header("Speed carpet variables")]
    #region BoostSpeed
    [SerializeField] float m_boostSpeedTimer;
    [SerializeField] float m_speedMultiplier;
    [SerializeField] float m_jumpBoost;
    float m_jumpBoostSave = 0f;
    float m_boostSpeedTimerSave = 0f;
    bool m_gonnaGoFast = false;
    #endregion
    [Space(2)]

    [Header("Slow carpet variables")]
    #region Slowspeed
    [SerializeField] float m_decreaseSpeedTimer;
    [SerializeField] float m_slowMultiplier;
    [SerializeField] float m_jumpDecrease;
    float m_jumpDecreaseSave = 0f;
    float m_decreaseSpeedTimerSave = 0f;
    bool m_gonnaGoSlow = false;
    #endregion
    [Space(2)]

    [Header("Wall run variables")]
    #region WallRun variables
    [SerializeField] float m_wallRunJumpMultiplier;
    [SerializeField] float m_yWallRunVelocity;
    [SerializeField] float m_jumpForceWall;
    [SerializeField] float m_wallJumpUpForce;
    [SerializeField] float m_gravityJumpFromWall;
    public LayerMask WhatIsWall;
    public GameObject Cube;
    public float WallRunForce;
    bool m_isWallRight, m_isWallLeft, m_isWallRunning;
    bool m_startWallRun = false;
    bool m_jumpFromWall = false;
    #endregion
    [Space(2)]

    [Header("Ground Check variables")]
    #region GroundCheck variables
    [SerializeField] float m_groundDistance;
    [SerializeField] float m_rayDownRange;
    RaycastHit m_hitGroundDistance;
    public Transform GroundCheck;
    public LayerMask GroundMask;
    bool m_isGrounded = true;
    bool m_groundHit;
    #endregion

    #region Rigidbody variables
    [SerializeField] float m_gravity;
    float m_groundedGravity = 0.05f;
    Rigidbody m_rb;
    Vector3 m_moveDir;
    #endregion
    [Space(2)]

    [Header("Player variables")]
    #region Player variables
    [SerializeField] float m_playerSpeed;
    [SerializeField] int m_howManyShapes;
    [SerializeField] float m_playerAirSpeed;
    [SerializeField] float m_jumpForce;
    public float Health;
    public GameObject Capsule;
    [HideInInspector] public bool IsMovingForward = false;
    [HideInInspector] public bool IsMoving = false;
    [HideInInspector] public bool DoubleJumpVar;
    int selectedShape = 0;
    //[SerializeField] float m_playerRunSpeed;
    //[SerializeField] int min = 0, max = 2;
    #endregion

    void Start()
    {
        //EventManager.BoostEvent += BoostSpeed;
        SelectedShape();
        m_rb = GetComponent<Rigidbody>();
        m_boostSpeedTimerSave = m_boostSpeedTimer;
        m_decreaseSpeedTimerSave = m_decreaseSpeedTimer;
        m_jumpBoostSave = m_jumpForce;
        m_jumpDecreaseSave = m_jumpForce;
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
        //Debug.Log("Doppio salto: " + DoubleJumpVar);
        //Debug.Log("Jumps: " + m_jumpsCount);
        //Debug.Log("Si sta muovendo?: " + IsMovingForward);
        //Debug.Log("Doppio salto: " + DoubleJumpVar);
        #endregion

        if (!m_isWallRunning && !m_gravityChange)
            ScrollShape();

        if (Cube.activeInHierarchy)
        {
            CheckForWall();
            JumpFromWall();
        } 
        else if (Cone.activeInHierarchy)
        {
            CheckGravityChange();
        }

        if (!Cone.activeInHierarchy)
            m_gravityChange = false;

        CheckJump();
        CheckGround();
        BoostSpeedTimer();
        DecreaseSpeedTimer();
        RestartScene();

        /*if (m_isWallRunning)
            m_rb.AddForce(Vector3.down * m_downForce, ForceMode.Force);*/
    }

    void FixedUpdate()
    {
        Movement();

        //Raycast that detects the gravity platform above the player
        m_platformUpHit = Physics.Raycast(transform.position, transform.up, out m_hitGravityPlatform, m_rayUpRange, PlatformUp);

        //Raycast that detects the distance between the player and the ground under the gravity platform
        m_groundHit = Physics.Raycast(transform.position, -transform.up, out m_hitGroundDistance, m_rayDownRange, GroundMask);
    }

    void CheckGround()
    {
        m_isGrounded = Physics.CheckSphere(GroundCheck.position, m_groundDistance, GroundMask);

        if(m_isGrounded)
        {
            Debug.DrawRay(transform.position, -transform.up * m_rayDownRange, Color.green);
            m_vCam1.SetActive(true);
            m_vCam2.SetActive(false);
        }
        else if(m_isOnTop)
        {
            m_vCam1.SetActive(false);
            m_vCam2.SetActive(true);
        }

        if(!m_isGrounded)
        {
            Debug.DrawRay(transform.position, -transform.up * m_rayDownRange, Color.red);
        }
    }

    #region Movement
    void Movement()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");
        m_moveDir = new Vector3(xMov, 0f, zMov);
        m_moveDir.Normalize();

        if (m_moveDir != Vector3.zero)
        {
            IsMoving = true;
            if (zMov > 0f)
                IsMovingForward = true;
            else
                IsMovingForward = false;
        }      
        else
            IsMoving = false;

        //If the player is on the ground
        if (m_isGrounded && !m_isWallRunning)
        {
            m_jumpFromWall = false;
            m_moveDir.x *= m_playerSpeed;
            m_moveDir.z *= m_playerSpeed;
            m_moveDir.y = m_rb.velocity.y;
            m_rb.AddForce(Vector3.down * m_groundedGravity, ForceMode.Acceleration);
            m_rb.velocity = m_moveDir;
            //m_rb.AddForce(m_moveDir, ForceMode.Force);
            //m_rb.velocity = Vector3.ClampMagnitude(m_rb.velocity, m_playerSpeed);
        }

        #region Different sphere movement

        /*else if (m_isGrounded && !m_isWallRunning && sphere.activeInHierarchy)
        {
            m_jumpFromWall = false;
            m_rb.AddForce(m_moveDir * 300 * Time.fixedDeltaTime, ForceMode.Acceleration);
            if (m_moveDir.sqrMagnitude < 0.01f) 
                m_rb.AddForce(-m_rb.velocity * m_playerSpeed, ForceMode.Acceleration);
        }*/
        #endregion

        //If the player is in air 
        else if (!m_isGrounded && !m_isWallRunning && !m_jumpFromWall)
        {
            m_moveDir.x *= m_playerAirSpeed;
            m_moveDir.z *= m_playerSpeed;
            m_moveDir.y = m_rb.velocity.y - m_gravity * Time.deltaTime;
            m_rb.velocity = m_moveDir;
        }
        //If the player is jumping from the wall
        else if (!m_isGrounded && m_jumpFromWall && !m_isWallRunning)
        {
            m_rb.velocity = new Vector3(m_rb.velocity.x, m_rb.velocity.y, m_rb.velocity.z);
            m_rb.AddForce(Vector3.down * m_gravityJumpFromWall, ForceMode.Acceleration);

        }
        //If the player is running on the wall
        else if (!m_isGrounded && !m_jumpFromWall && m_isWallRunning)
        {
            //The player automatically goes forward while running on the wall
            m_rb.velocity = new Vector3(0f, 0f, m_playerSpeed);

            //Let the player go up and down while running on the wall
            //Up
            if (Input.GetKey(KeyCode.E))
            {
                m_rb.velocity = new Vector3(0f, m_yWallRunVelocity, m_rb.velocity.z);
            }
            //Down
            if (Input.GetKey(KeyCode.Q))
            {
                m_rb.velocity = new Vector3(0f, -m_yWallRunVelocity, m_rb.velocity.z);
            }
        }
    }
    #endregion

    #region GravityChange
    void CheckGravityChange()
    {
        if (m_platformUpHit || m_isOnTop)
        {
            GravityChange();
            Debug.DrawRay(transform.position, transform.up * m_rayUpRange, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.up * m_rayUpRange, Color.red);
            if (m_gravityChange)
            {
                StopGravityPlatform();
            }
        }       
    }

    void GravityChange()
    {
        Debug.Log("GravityChange: " + m_gravityChange);
        if (Input.GetKeyDown(KeyCode.E) && (m_isGrounded || !m_groundHit))
        {
            if (!m_isOnTop && !m_gravityChange)
            {
                ConeAnim.Play("ConeRotationUp");
                m_goUp = true;
                m_gravityChange = true;
                m_rb.AddForce(transform.up * m_magneticForce, ForceMode.Force);
                m_gravity *= -1;
                Debug.Log("Vado su");
                m_vCam1.SetActive(false);
                m_vCam2.SetActive(true);
            }
            else if(m_isOnTop)
            {
                ConeAnim.Play("ConeRotationDown");
                m_goUp = false;
                m_gravityChange = false;
                m_rb.AddForce(-transform.up * m_magneticForce, ForceMode.Force);
                m_gravity *= -1;
                Debug.Log("Vado giù");
                m_vCam1.SetActive(true);
                m_vCam2.SetActive(false);
            }
            else if(m_goUp && !m_isGrounded)
            {
                ConeAnim.Play("ConeRotationDown");
                m_goUp = false;
                m_gravityChange = false;
                m_rb.AddForce(-transform.up * m_magneticForce, ForceMode.Force);
                m_gravity *= -1;
                Debug.Log("Torno giù dopo essere andato su");
                m_vCam1.SetActive(true);
                m_vCam2.SetActive(false);
            }
        }
    }

    void StopGravityPlatform()
    {
        ConeAnim.Play("ConeRotationDown");
        m_rb.AddForce(-transform.up * m_magneticForce, ForceMode.Force);
        m_gravity *= -1;
        m_gravityChange = false;
        m_vCam1.SetActive(true);
        m_vCam2.SetActive(false);
    }
    #endregion

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
            m_rb.velocity = Vector3.zero;
            //m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            m_rb.velocity = new Vector3(m_rb.velocity.x, m_jumpForce, m_rb.velocity.z);

            if (Capsule.activeInHierarchy)
                DoubleJumpVar = true;
        }
    }
    
    void DoubleJump()
    {
        if (DoubleJumpVar && !m_isWallRunning && !m_jumpFromWall && Input.GetKeyDown(KeyCode.Space))
        {
            m_rb.velocity = Vector3.zero;
            m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            DoubleJumpVar = false;
        }
    }
    #endregion

    #region WallRun
    void CheckForWall()
    {
        m_isWallRight = Physics.Raycast(transform.position, transform.right, 1.5f, WhatIsWall);
        m_isWallLeft = Physics.Raycast(transform.position, -transform.right, 1.5f, WhatIsWall);

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
            //DoubleJumpVar = false;

            //Make sure the player sticks to the wall
            if (m_isWallRight)
            {
                m_rb.AddForce(transform.right * WallRunForce, ForceMode.Force);
            }
            else if (m_isWallLeft)
            {
                m_rb.AddForce(-transform.right * WallRunForce, ForceMode.Force);
            }
        }
    }

    void JumpFromWall()
    {
        if (m_isWallRunning && Input.GetKeyDown(KeyCode.Space))
        {
            m_jumpFromWall = true;
            
            if (m_isWallRight && !Input.GetKey(KeyCode.W))
            {
                /*m_rb.AddForce(m_wallRunJumpMultiplier * m_jumpForceWall * -transform.right, ForceMode.Force);
                m_rb.AddForce(transform.up * m_jumpForceWall * m_wallJumpUpForce);*/
                float angle = -90f;
                Vector3 diagonalDir = Quaternion.Euler(angle * Vector3.up) * transform.forward;
                m_rb.AddForce(diagonalDir * m_jumpForceWall, ForceMode.Impulse);
            }
            else if (m_isWallRight && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)))
            {
                float angle = -15f;
                Vector3 diagonalDir = Quaternion.Euler(angle * Vector3.up) * transform.forward;
                m_rb.AddForce(diagonalDir * m_jumpForceWall, ForceMode.Impulse);
            }

            if (m_isWallLeft && !Input.GetKey(KeyCode.W))
            {
                float angle = 90f;
                Vector3 diagonalDir = Quaternion.Euler(angle * Vector3.up) * transform.forward;
                m_rb.AddForce(diagonalDir * m_jumpForceWall, ForceMode.Impulse); ;
            }
            else if (m_isWallLeft && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D)))
            {
                float angle = 15f;
                Vector3 diagonalDir = Quaternion.Euler(angle * Vector3.up) * transform.forward;
                m_rb.AddForce(diagonalDir * m_jumpForceWall, ForceMode.Impulse);
            }

            m_isWallRunning = false;
        }
    }
    #endregion

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

    #region SlowSpeed
    void DecreaseSpeedTimer()
    {
        if (m_gonnaGoSlow)
        {
            m_decreaseSpeedTimer -= Time.deltaTime;
            if (m_decreaseSpeedTimer <= 0.01f)
            {
                m_decreaseSpeedTimer = m_decreaseSpeedTimerSave;
                m_playerSpeed *= m_slowMultiplier;
                m_jumpForce = m_jumpDecreaseSave;
                m_gonnaGoSlow = false;
            }
        }
    }

    void DecreaseSpeed()
    {
        if (!m_gonnaGoSlow)
        {
            m_playerSpeed /= m_slowMultiplier;
            m_jumpForce = m_jumpDecrease;
            m_gonnaGoSlow = true;
        }
    }
    #endregion

    #region OnTrigger/OnCollision
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedPlatform"))
        {
            BoostSpeed();
            //EventManager.StartBoost();
        }
        
        if(other.CompareTag("SlowPlatform"))
        {
            DecreaseSpeed();
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
    #endregion

    void RestartScene()
    {
        if (Health <= 0f)
        { 
            SceneManager.LoadScene(0);
        }
    }
}
