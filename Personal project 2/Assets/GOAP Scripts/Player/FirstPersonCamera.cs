using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour {
    public Transform target;
    private float cameraRotateSpeedX = 250.0f;
    private float mouseX;
    private Vector3 cameraRotation = Vector3.zero;
    private float cameraRotateSpeedY = 100.0f;
    private float minCameraAngleY = -20.0f;
    private float maxCameraAngleY = 40.0f;




    void LateUpdate() {
        //cameraRotation.x += Input.GetAxis("Mouse Y") * cameraRotateSpeedY * Time.deltaTime * -1;
        //cameraRotation.x = Mathf.Clamp(cameraRotation.x, minCameraAngleY, maxCameraAngleY);

        //mouseX = Input.GetAxis("Mouse X") * cameraRotateSpeedX * Time.deltaTime;
        //target.transform.Rotate(0.0f, mouseX, 0.0f);

        //transform.localEulerAngles = cameraRotation;
    }

}
