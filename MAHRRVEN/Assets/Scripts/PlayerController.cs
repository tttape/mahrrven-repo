using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;

    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;

    [Header("Jump Settings")]
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float gravity = -20f;

    public GameObject tunnelBUI;

    public CharacterController characterController;

    private bool isGrounded;
    private float ySpeed;

    //jump
    private bool isJumping;

    private Quaternion targetRotation;

    private Animator animator;
    private CameraController cameraController;
    //public CharacterController characterController;
    

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponentInChildren<Animator>();

        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            return;
        }

        HandleCursor();

        

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Abs(h) + Mathf.Abs(v);

        var moveInput = (new Vector3(h, 0, v)).normalized;

        var moveDir = cameraController.PlanarRotation * moveInput;
        moveDir.Normalize();

        GroundCheck();
        //Debug.Log("isGrounded = " + isGrounded);
        if (isGrounded)
        {
            //ySpeed = -0.5f;

            ySpeed = -2f;

            // Reset jump on landing
            if (isJumping)
            {
                isJumping = false;
                animator?.SetBool("IsJumping", false);
            }

            // Jump input
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpForce;
                isJumping = true;
                animator?.SetBool("IsJumping", true);
            }
        }
        else
        {
            //ySpeed += Physics.gravity.y * Time.deltaTime;

            ySpeed += gravity * Time.deltaTime;
        }


        //debug
        //if (isGrounded)
        //{
        //    ySpeed = -2f;
        //    Debug.Log("Player is grounded");

        //    if (isJumping)
        //    {
        //        isJumping = false;
        //        animator?.SetBool("IsJumping", false);
        //    }

        //    if (Input.GetButtonDown("Jump"))
        //    {
        //        Debug.Log("Jump pressed!");
        //        ySpeed = jumpForce;
        //        isJumping = true;
        //        animator?.SetBool("IsJumping", true);
        //    }
        //}
        //else
        //{
        //    Debug.Log("Player is NOT grounded");
        //    ySpeed += gravity * Time.deltaTime;
        //}

        var velocity = moveDir * moveSpeed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);


        if (moveAmount > 0)
        {
            targetRotation = Quaternion.LookRotation(moveDir);

        }

        //transform.position += moveDir * moveSpeed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,
            rotationSpeed * Time.deltaTime);

        animator?.SetFloat("Speed", moveAmount, 0.1f, Time.deltaTime);
    }

    private void HandleCursor()
    {
        if (tunnelBUI.activeSelf || PauseMenu.GameIsPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }

    public CharacterController CharacterControllerRef => characterController;

    public void Teleport(Transform spawnPoint)
    {
        if (characterController != null)
        {
            characterController.enabled = false;
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
            characterController.enabled = true;
        }
        else
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }
    }

    

}
