using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;

    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;

    public GameObject tunnelBUI;

    public CharacterController characterController;

    private bool isGrounded;
    private float ySpeed;
    private Quaternion targetRotation;

    private CameraController cameraController;
    //public CharacterController characterController;
    

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();

        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused) return;

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
            ySpeed = -0.5f;
        }
        else
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }

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

    //public void TeleportToTunnelB(Transform tunnelBSpawn)
    //{
    //    if (characterController != null)
    //    {
    //        characterController.enabled = false;
    //        transform.position = tunnelBSpawn.position;
    //        transform.rotation = tunnelBSpawn.rotation; // optional
    //        characterController.enabled = true;
    //    }
    //    else
    //    {
    //        transform.position = tunnelBSpawn.position;
    //        transform.rotation = tunnelBSpawn.rotation;
    //    }
    //}

    //public void ReturnToTunnelC(Transform tunnelCSpawn)
    //{
    //    if (characterController != null)
    //    {
    //        characterController.enabled = false;
    //        transform.position = tunnelCSpawn.position;
    //        transform.rotation = tunnelCSpawn.rotation;
    //        characterController.enabled = true;
    //    }
    //    else
    //    {
    //        transform.position = tunnelCSpawn.position;
    //        transform.rotation = tunnelCSpawn.rotation;
    //    }

    //    tunnelBUI.SetActive(false);
    //}

}
