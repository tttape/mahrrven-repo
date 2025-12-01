using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;

    Quaternion targetRotation;

    CameraController cameraController;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Abs(h) + Mathf.Abs(v);

        var moveInput = (new Vector3(h, 0, v)).normalized;

        var moveDir = cameraController.PlanarRotation * moveInput;
        moveDir.Normalize();

        if (moveAmount > 0)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;

            
            targetRotation = Quaternion.LookRotation(moveDir);
            



        }

        //transform.position += moveDir * moveSpeed * Time.deltaTime;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,
                rotationSpeed * Time.deltaTime);
        }
}
