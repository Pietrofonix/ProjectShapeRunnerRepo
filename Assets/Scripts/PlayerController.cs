using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Sphere variables")]
    #region GhostObj variables
    //[SerializeField] GameObject[] m_ghostPlatform;
    //[SerializeField] GameObject[] m_normalPlatform;
    [SerializeField] Transform m_switchPlatforms1;
    [SerializeField] Transform m_switchPlatforms2;
    public GameObject Sphere;
    bool m_sphereButton;
    #endregion
    [Space(2)]

    [Header("GravityChange variables")]
    #region Gravity variables
    [SerializeField] float m_magneticForce;
    [SerializeField] float m_rayUpRange;
    [SerializeField] GameObject m_vCam1;
    [SerializeField] GameObject m_vCam2;
    [SerializeField] float m_pressTime;
    RaycastHit m_hitGravityPlatform;
    public Animator ConeAnim;
    public LayerMask PlatformUp;
    public GameObject Cone;
    bool m_gravityChange = false;
    bool m_startGravityChange = false;
    bool m_isOnTop = false;
    bool m_goUp = false;
    bool m_platformUpHit;
    float m_pressTimer;
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
    //[SerializeField] float m_wallRunJumpMultiplier;
    //[SerializeField] float m_gravityForwardJumpFromWall;
    [SerializeField] float m_yWallRunVelocity;
    [SerializeField] float m_jumpForceWall;
    [SerializeField] float m_forwardJumpForceWall;
    [SerializeField] float m_forwardJumpUpForceWall;
    [SerializeField] float m_wallJumpUpForce;
    [SerializeField] float m_gravityJumpFromWall;
    [SerializeField] float m_wallRunForce;
    [SerializeField] float m_wallRunRaycastLength;
    [SerializeField] ShapesWheelController m_shapesWheelController;
    public LayerMask WhatIsWall;
    public GameObject Cube;
    RaycastHit m_wallRunDetection;
    bool m_isWallRight;
    bool m_isWallLeft;
    bool m_isWallRunning;
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
    #endregion

    #region Rigidbody variables
    Rigidbody m_rb;
    Vector3 m_moveDir;
    float m_groundedGravity = 0.05f;
    #endregion
    [Space(2)]

    [Header("Player variables")]
    #region Player variables
    [SerializeField] float m_gravity;
    [SerializeField] float m_zPlayerSpeed;
    [SerializeField] float m_xPlayerSpeed;
    [SerializeField] int m_howManyShapes;
    [SerializeField] float m_playerAirSpeed;
    [SerializeField] float m_jumpForce;
    [SerializeField] TrailRenderer[] m_trailRenderers;
    public GameObject Capsule;
    [HideInInspector] public bool IsMovingForward = false;
    [HideInInspector] public bool IsMoving = false;
    [HideInInspector] public bool DoubleJumpVar;
    float m_playerSpeedSave = 0f;
    int selectedShape = 0;
    #endregion

    void Start()
    {
        if (m_switchPlatforms1 != null)
        {
            foreach (Transform ghostPlatform in m_switchPlatforms1)
            {
                Color trasparentColor = ghostPlatform.GetComponent<MeshRenderer>().material.color;
                trasparentColor.a = 0.3f;
                ghostPlatform.GetComponent<MeshRenderer>().material.color = trasparentColor;
                //ghostPlatform.GetComponent<MeshRenderer>().enabled = false;
                ghostPlatform.GetComponent<BoxCollider>().enabled = false;
            }
        }

        //SelectedShape();
        m_sphereButton = false;
        m_rb = GetComponent<Rigidbody>();
        m_boostSpeedTimerSave = m_boostSpeedTimer;
        m_decreaseSpeedTimerSave = m_decreaseSpeedTimer;
        m_jumpBoostSave = m_jumpForce;
        m_jumpDecreaseSave = m_jumpForce;
        m_playerSpeedSave = m_zPlayerSpeed;
        m_pressTimer = 0f;
    }

    void Update()
    {
        #region Debug
        //Debug.Log("m_gravityChange: " + m_gravityChange);
        //Debug.Log("m_isOnTop: " + m_isOnTop);
        //Debug.Log("StartWallRun: " + m_startWallRun);
        //Debug.Log("Wall Run= " + m_isWallRunning);
        //Debug.Log(m_rb.velocity);
        //Debug.Log("Il salto dal muro è: " + m_jumpFromWall);
        //Debug.Log("Sono a terra: " + m_isGrounded);
        //Debug.Log("Doppio salto: " + DoubleJumpVar);
        //Debug.Log("Jumps: " + m_jumpsCount);
        //Debug.Log("Si sta muovendo?: " + IsMovingForward);
        //Debug.Log("Doppio salto: " + DoubleJumpVar);
        //Debug.Log("Sto andando veloce: " + m_gonnaGoFast);
        //Debug.Log("Sto andando piano: " + m_gonnaGoSlow);
        //Debug.Log("Muro a sinistra: " + m_isWallLeft);
        //Debug.Log("SpeedTimer: " + m_boostSpeedTimer);
        //Debug.Log(m_pressTimer);
        //Debug.Log("Ruota: " + m_shapesWheelController.ActivateWheel);
        //Debug.Log("Gravità: " + m_gravity);
        #endregion

        if (m_switchPlatforms1 == null && m_switchPlatforms2 == null)
        {
            m_switchPlatforms1 = null;
            m_switchPlatforms2 = null;
        }

        if (!Cone.activeInHierarchy)
        {
            m_gravityChange = false;
            Cone.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            CheckGravityChange();
        }

        if (Cube.activeInHierarchy)
        {
            JumpFromWall();
        }

        if (Sphere.activeInHierarchy && Input.GetKeyDown(KeyCode.E) && !m_sphereButton)
        {
            GhostPlatformOn();
            m_sphereButton = true;
        }
        else if (Sphere.activeInHierarchy && Input.GetKeyDown(KeyCode.E) && m_sphereButton)
        {
            GhostPlatformOff();
            m_sphereButton = false;
        }

        CheckJump();
        CheckGround();
        BoostSpeedTimer();
        DecreaseSpeedTimer();

        //Make the shape rotated correctly based on the surface is moving on
        transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.right, m_hitGroundDistance.normal));
    }

    void FixedUpdate()
    {
        Movement();

        if (Cube.activeInHierarchy)
        {
            CheckForWall();
        }
        else if (Cone.activeInHierarchy)
        {
            GravityChange();
        }

        if (!Cone.activeInHierarchy && m_gravity < 0f)
        {
            m_rb.AddForce(-transform.up * m_magneticForce, ForceMode.Acceleration);
            m_gravity = -m_gravity;
            m_goUp = false;
            m_isOnTop = false;
            m_gravityChange = false;
            m_vCam1.SetActive(true);
            m_vCam2.SetActive(false);
        }

        //Raycasts that detect the walls on the right/left side
        m_isWallRight = Physics.Raycast(transform.position + transform.up * -0.5f, transform.right, out m_wallRunDetection, m_wallRunRaycastLength, WhatIsWall);
        m_isWallLeft = Physics.Raycast(transform.position + transform.up * -0.5f, -transform.right, out m_wallRunDetection, m_wallRunRaycastLength, WhatIsWall);

        //Raycast that detects the gravity platform above the player
        m_platformUpHit = Physics.Raycast(transform.position, transform.up, out m_hitGravityPlatform, m_rayUpRange, PlatformUp);

        #region Raycast debug
        if (m_isWallRight) 
            Debug.DrawRay(transform.position + transform.up * -0.5f, transform.right * m_wallRunRaycastLength, Color.yellow);
        else
            Debug.DrawRay(transform.position + transform.up * -0.5f, transform.right * m_wallRunRaycastLength, Color.red);

        if (m_isWallLeft)
            Debug.DrawRay(transform.position + transform.up * -0.5f, -transform.right * m_wallRunRaycastLength, Color.blue);
        else
            Debug.DrawRay(transform.position + transform.up * -0.5f, -transform.right * m_wallRunRaycastLength, Color.red);
        #endregion
    }

    void CheckGround()
    {
        m_isGrounded = Physics.CheckSphere(GroundCheck.position, m_groundDistance, GroundMask);

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

    public void NormalSpeed()
    {
        foreach (TrailRenderer trail in m_trailRenderers)
        {
            trail.enabled = false;
        }

        m_zPlayerSpeed = m_playerSpeedSave;
        m_jumpForce = m_jumpBoostSave;
        m_boostSpeedTimer = m_boostSpeedTimerSave;
        m_decreaseSpeedTimer = m_decreaseSpeedTimerSave;
        m_gonnaGoSlow = false;
        m_gonnaGoFast = false;
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
            {
                IsMovingForward = true;
            }
            else
            {
                IsMovingForward = false;
            }
        }      
        else
        {
            IsMoving = false;
        }

        //If the player is on the ground
        if (m_isGrounded && !m_isWallRunning)
        {
            m_jumpFromWall = false;
            m_moveDir.x *= m_xPlayerSpeed;
            m_moveDir.z *= m_zPlayerSpeed;
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
            m_moveDir.z *= m_zPlayerSpeed;
            m_moveDir.y = m_rb.velocity.y - m_gravity * Time.fixedDeltaTime;
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
            m_rb.velocity = new Vector3(0f, 0f, m_zPlayerSpeed);

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
        if (m_platformUpHit || (m_isOnTop && m_gravityChange))
        {
            Debug.DrawRay(transform.position, transform.up * m_rayUpRange, Color.green);
            m_pressTimer -= Time.deltaTime;

            if (m_pressTimer <= 0f)
            {
                m_pressTimer = 0f;
            }

            if (m_isOnTop && m_gravityChange)
            {
                m_shapesWheelController.ShapesWheelIsActive = false;
            }

            if (Input.GetKeyDown(KeyCode.E) && m_pressTimer <= 0.01f)
            {
                m_startGravityChange = true;
                m_pressTimer = m_pressTime;
            }
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
        if (m_startGravityChange && Cone.activeInHierarchy)
        {
            m_startGravityChange = false;
            if (!m_isOnTop && !m_gravityChange)
            {
                ConeAnim.Play("ConeRotationUp");
                m_goUp = true;
                m_gravityChange = true;
                m_rb.AddForce(transform.up * m_magneticForce, ForceMode.Acceleration);
                m_gravity = -m_gravity;
                m_vCam1.SetActive(false);
                m_vCam2.SetActive(true);
            }
            else if ((m_isOnTop || !m_isOnTop) && m_gravityChange)
            {
                ConeAnim.Play("ConeRotationDown");
                m_goUp = false;
                m_gravityChange = false;
                m_rb.AddForce(-transform.up * m_magneticForce, ForceMode.Acceleration);
                m_gravity = -m_gravity;
                m_vCam1.SetActive(true);
                m_vCam2.SetActive(false);
            }
        }

        /*if (m_goUp)
        {
            m_shapesWheelController.ShapesWheelIsActive = false;
        }*/
    }

    void StopGravityPlatform()
    {
        //m_pressTimer = 0f;
        ConeAnim.Play("ConeRotationDown");
        m_rb.AddForce(-transform.up * m_magneticForce, ForceMode.Acceleration);
        m_gravity = -m_gravity;
        m_goUp = false;
        m_gravityChange = false;
        m_vCam1.SetActive(true);
        m_vCam2.SetActive(false);
    }
    #endregion

    #region OldChangeShape
    void ScrollShape()
    {
        int previousSelectedShape = selectedShape;

        if (/*Input.GetAxis("Mouse ScrollWheel") > 0f*/ Input.GetMouseButtonDown(0)) // forward
        {
            if (selectedShape >= transform.childCount - (m_howManyShapes - 1))
            {
                selectedShape = 0;
            }
            else
            {
                selectedShape++;
            }
        }

        if (/*Input.GetAxis("Mouse ScrollWheel") < 0f*/ Input.GetMouseButtonDown(1)) // backwards
        {
            if (selectedShape <= 0)
            {
                selectedShape = transform.childCount - (m_howManyShapes - 1);
            }
            else
            {
                selectedShape--;
            }
        }

        /*if (previousSelectedShape != selectedShape)
        {
            SelectedShape();
        }*/
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
                {
                    shape.gameObject.SetActive(false);
                }
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

        if (m_isOnTop)
        {
            ReverseJump();
        }
        else if (!m_isOnTop && m_gravityChange)
        {
            ReverseDoubleJump();
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_rb.velocity = Vector3.zero;
            //m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            m_rb.velocity = new Vector3(m_rb.velocity.x, m_jumpForce, m_rb.velocity.z);
            DoubleJumpVar = true;
        }
    }
    
    void DoubleJump()
    {
        if (DoubleJumpVar && Capsule.activeInHierarchy && !m_isWallRunning && !m_jumpFromWall && Input.GetKeyDown(KeyCode.Space))
        {
            m_rb.velocity = Vector3.zero;
            //m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            m_rb.velocity = new Vector3(m_rb.velocity.x, m_jumpForce, m_rb.velocity.z);
            DoubleJumpVar = false;
        }
    }

    void ReverseJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_rb.velocity = Vector3.zero;
            m_rb.velocity = new Vector3(m_rb.velocity.x, -m_jumpForce, m_rb.velocity.z);
            DoubleJumpVar = true;
        }
    }

    void ReverseDoubleJump()
    {
        if (DoubleJumpVar && Cone.activeInHierarchy && !m_isWallRunning && !m_jumpFromWall && Input.GetKeyDown(KeyCode.Space))
        {
            m_rb.velocity = Vector3.zero;
            //m_rb.AddForce(Vector3.down * m_jumpForce, ForceMode.Impulse);
            m_rb.velocity = new Vector3(m_rb.velocity.x, -m_jumpForce, m_rb.velocity.z);
            DoubleJumpVar = false;
        }
    }
    #endregion

    #region WallRun
    void CheckForWall()
    {
        //Leave wall run
        if (!m_isWallRight && !m_isWallLeft)
        {
            m_shapesWheelController.ShapesWheelIsActive = true;
            StopWallRun();
            m_startWallRun = false;
        }
        else
        {
            m_shapesWheelController.ShapesWheelIsActive = false;
            StartWallRun();
        }
    }

    void StopWallRun()
    {
        m_isWallRunning = false;
    }

    void StartWallRun()
    {
        if(!m_startWallRun && !m_isGrounded)
        {
            m_startWallRun = true;
            m_jumpFromWall = false;
            m_isWallRunning = true;

            //Make sure the player sticks to the wall
            if (m_isWallRight)
            {
                m_rb.AddForce(transform.right * m_wallRunForce, ForceMode.Acceleration);
            }
            else if (m_isWallLeft)
            {
                m_rb.AddForce(-transform.right * m_wallRunForce, ForceMode.Acceleration);
            }
        }
    }

    void JumpFromWall()
    {
        if (m_isWallRunning && Input.GetKeyDown(KeyCode.Space))
        {
            m_jumpFromWall = true;
            
            if (m_isWallRight /*&& !Input.GetKey(KeyCode.W)) || (m_isWallRight &&  Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))*/)
            {
                /*m_rb.AddForce(m_wallRunJumpMultiplier * m_jumpForceWall * -transform.right, ForceMode.Force);
                m_rb.AddForce(transform.up * m_jumpForceWall * m_wallJumpUpForce);*/
                float angle = -90f;
                Vector3 diagonalDir = Quaternion.Euler(angle * Vector3.up) * transform.forward;
                m_rb.AddForce(diagonalDir * m_jumpForceWall, ForceMode.Impulse);
            }
            /*else if (m_isWallRight && (Input.GetKey(KeyCode.W)*//* || Input.GetKey(KeyCode.A)*//*))
            {
                float angle = -15f;
                Vector3 diagonalDir = Quaternion.Euler(angle * Vector3.up) * transform.forward;
                m_rb.AddForce(diagonalDir * m_jumpForceWall, ForceMode.Impulse);
            }*/

            if (m_isWallLeft/* && !Input.GetKey(KeyCode.W)) || (m_isWallLeft && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))*/)
            {
                float angle = 90f;
                Vector3 diagonalDir = Quaternion.Euler(angle * Vector3.up) * transform.forward;
                m_rb.AddForce(diagonalDir * m_jumpForceWall, ForceMode.Impulse); 
            }
            /*else if (m_isWallLeft && (Input.GetKey(KeyCode.W)*//* || Input.GetKey(KeyCode.D)*//*))
            {
                float angle = 15f;
                Vector3 diagonalDir = Quaternion.Euler(angle * Vector3.up) * transform.forward;
                m_rb.AddForce(diagonalDir * m_jumpForceWall, ForceMode.Impulse);
            }*/

            m_rb.AddForce(transform.up * m_wallJumpUpForce, ForceMode.Impulse);
            m_isWallRunning = false;
        }
    }
    #endregion

    #region PlatformOnOff
    void GhostPlatformOn()
    {
        if (m_switchPlatforms1 != null & m_switchPlatforms2 != null)
        {
            foreach (Transform normalPlatform in m_switchPlatforms2)
            {
                Color transparentColor = normalPlatform.GetComponent<MeshRenderer>().material.color;
                transparentColor.a = 0.3f;
                normalPlatform.GetComponent<MeshRenderer>().material.color = transparentColor;
                normalPlatform.GetComponent<BoxCollider>().enabled = false;
            }

            foreach (Transform ghostPlatform in m_switchPlatforms1)
            {
                Color transparentColor = ghostPlatform.GetComponent<MeshRenderer>().material.color;
                transparentColor.a = 1f;
                ghostPlatform.GetComponent<MeshRenderer>().material.color = transparentColor;
                ghostPlatform.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    void GhostPlatformOff()
    {
        if (m_switchPlatforms1 != null && m_switchPlatforms2 != null)
        {
            foreach (Transform normalPlatform in m_switchPlatforms2)
            {
                Color transparentColor = normalPlatform.GetComponent<MeshRenderer>().material.color;
                transparentColor.a = 1f;
                normalPlatform.GetComponent<MeshRenderer>().material.color = transparentColor;
                normalPlatform.GetComponent<BoxCollider>().enabled = true;
            }

            foreach (Transform ghostPlatform in m_switchPlatforms1)
            {
                Color transparentColor = ghostPlatform.GetComponent<MeshRenderer>().material.color;
                transparentColor.a = 0.3f;
                ghostPlatform.GetComponent<MeshRenderer>().material.color = transparentColor;
                ghostPlatform.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
    #endregion

    #region BoostSpeed
    void BoostSpeedTimer()
    {
        if (m_gonnaGoFast)
        {
            foreach(TrailRenderer trail in m_trailRenderers)
            {
                trail.enabled = true;
                trail.startColor = Color.blue;
                trail.endColor = Color.white;
            }

            m_boostSpeedTimer -= Time.deltaTime;

            if (m_boostSpeedTimer <= 0.01f)
            {
                foreach (TrailRenderer trail in m_trailRenderers)
                {
                    trail.enabled = false;
                }
                m_boostSpeedTimer = m_boostSpeedTimerSave;
                m_zPlayerSpeed /= m_speedMultiplier;
                m_jumpForce = m_jumpBoostSave;
                m_gonnaGoFast = false;
            }
        }
    }

    void BoostSpeed()
    {
        if (!m_gonnaGoFast)
        {
            m_zPlayerSpeed *= m_speedMultiplier;
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
            foreach (TrailRenderer trail in m_trailRenderers)
            {
                trail.enabled = true;
                trail.startColor = Color.red;
                trail.endColor = Color.white;
            }

            m_decreaseSpeedTimer -= Time.deltaTime;
            if (m_decreaseSpeedTimer <= 0.01f)
            {
                foreach (TrailRenderer trail in m_trailRenderers)
                {
                    trail.enabled = false;
                }

                m_decreaseSpeedTimer = m_decreaseSpeedTimerSave;
                m_zPlayerSpeed *= m_slowMultiplier;
                m_jumpForce = m_jumpDecreaseSave;
                m_gonnaGoSlow = false;
            }
        }
    }

    void DecreaseSpeed()
    {
        if (!m_gonnaGoSlow)
        {
            m_zPlayerSpeed /= m_slowMultiplier;
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
            if (!m_gonnaGoSlow)
            {
                BoostSpeed();
            }
            else
            {
                NormalSpeed();
            }

            if (m_gonnaGoFast)
            {
                m_boostSpeedTimer = m_boostSpeedTimerSave;
            }
        }
        
        if(other.CompareTag("SlowPlatform"))
        {
            if (!m_gonnaGoFast)
            {
                DecreaseSpeed();
            }
            else
            {
                NormalSpeed();
            }

            if (m_gonnaGoSlow)
            {
                m_decreaseSpeedTimer = m_decreaseSpeedTimerSave;
            }
        }

        if (other.CompareTag("EndOfWall") && !m_jumpFromWall)
        {
            if (m_isWallLeft)
            {
                float angle = 15f;
                Vector3 diagonalDir = Quaternion.Euler(angle * Vector3.up) * transform.forward;
                m_rb.AddForce(diagonalDir * m_forwardJumpForceWall, ForceMode.Impulse);
                m_rb.AddForce(transform.up * m_forwardJumpUpForceWall, ForceMode.Impulse);
            }

            if (m_isWallRight)
            {
                float angle = -15f;
                Vector3 diagonalDir = Quaternion.Euler(angle * Vector3.up) * transform.forward;
                m_rb.AddForce(diagonalDir * m_forwardJumpForceWall, ForceMode.Impulse);
                m_rb.AddForce(transform.up * m_forwardJumpUpForceWall, ForceMode.Impulse);
            }

            m_rb.velocity = new Vector3(0f, 0f, m_zPlayerSpeed);
            m_isWallRunning = false;
            m_jumpFromWall = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!Cube.activeInHierarchy && (other.gameObject.CompareTag("TopEdge") || other.gameObject.CompareTag("BottomEdge")))
        {
            other.isTrigger = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GravityPlatform"))
        {
            m_isOnTop = true;
        }

        if (!Cube.activeInHierarchy && (collision.gameObject.CompareTag("TopEdge") || collision.gameObject.CompareTag("BottomEdge")))
        {
            collision.collider.isTrigger = true;
        }

        if (collision.gameObject.CompareTag("Ground") && Cone.activeInHierarchy && m_gravityChange)
        {
            StopGravityPlatform();
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
}