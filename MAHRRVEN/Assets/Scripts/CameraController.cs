using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;


    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float distance = 5;
    //[SerializeField] float height = 0;

    [SerializeField] float minVerticalAngle = -45;
    [SerializeField] float maxVerticalAngle = 45;

    [SerializeField] Vector2 framingOffset;

    [SerializeField] bool invertX;
    [SerializeField] bool invertY;


    float rotationX;
    float rotationY;

    float invertXVal;
    float invertYVal;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (PauseMenu.GameIsPaused) return;

        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;

        rotationX += Input.GetAxis("Mouse Y") * invertYVal * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        //rotationX += Input.GetAxis("Mouse Y") * invertYVal * rotationSpeed * Time.deltaTime;
        //rotationY += Input.GetAxis("Mouse X") * invertXVal * rotationSpeed * Time.deltaTime;

        rotationY += Input.GetAxis("Mouse X") * invertXVal * rotationSpeed;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);


        var focusPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);

        transform.position = focusPosition - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;
    }

    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}
