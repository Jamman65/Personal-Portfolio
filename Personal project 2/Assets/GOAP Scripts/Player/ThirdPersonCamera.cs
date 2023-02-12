using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour {
    public enum CameraLocations { Normal, Stand, Crouch, Kneel, Prone }
    private CameraLocations cameraLocation;
    public Transform target;
    private float cameraRotateSpeedX = 250.0f;
    private float mouseX;
    private Vector3 cameraRotation = Vector3.zero;
    private float cameraRotateSpeedY = 100.0f;
    private float minCameraAngleY = -20.0f;
    private float maxCameraAngleY = 40.0f;
    public Transform cameraNormalPosition;
    public Transform cameraCloseStandPosition;
    public Transform cameraCrouchPosition;
    public Transform cameraCloseCrouchPosition;
    public Transform cameraClosePronePosition;
    private Transform cameraClosePosition;
    private float smooth = 3.0f;
    private float mouseZ;
    private float minViewingAngle = 20.0f;
    private float normalFieldofView;
    private Camera camera;
    private WeaponController weaponController;




    void Start() {
        camera = GetComponent<Camera>();
        normalFieldofView = camera.fieldOfView;
        cameraLocation = CameraLocations.Normal;
        SetCameraPosition(cameraLocation);
        weaponController = target.GetComponent<WeaponController>();
    }


    void LateUpdate() {
        // Exercise 5.3
        if (target != null) {
            //cameraRotation.x += Input.GetAxis("Mouse Y") * cameraRotateSpeedY * Time.deltaTime * -1;
            cameraRotation.x = Mathf.Clamp(cameraRotation.x, minCameraAngleY, maxCameraAngleY);
            //mouseX = Input.GetAxis("Mouse X") * cameraRotateSpeedX * Time.deltaTime;
            target.transform.Rotate(0.0f, mouseX, 0.0f);
            //if (//Input.GetButton("Fire2")) {
            //    cameraClosePosition.transform.localEulerAngles = new Vector3(cameraRotation.x, cameraClosePosition.transform.localEulerAngles.y, cameraClosePosition.transform.localEulerAngles.z);
            //    transform.position = Vector3.Lerp(transform.position, cameraClosePosition.position, Time.deltaTime * smooth);
            //    transform.forward = Vector3.Lerp(transform.forward, cameraClosePosition.forward, Time.deltaTime * smooth);
            //    if (weaponController && weaponController.useScope) {
            //        // Change the field of view based on the mouse wheel (zoom) value
            //       // mouseZ = Input.GetAxis("Zoom") * (Time.deltaTime / 4.0f);
            //        // pos mouse reduces fov
            //        if (mouseZ > 0) {
            //            // Zoom in
            //            camera.fieldOfView = Mathf.MoveTowards(camera.fieldOfView, minViewingAngle, mouseZ);
            //        }
            //        else {
            //            // Zoom out
            //            mouseZ *= -1.0f;
            //            camera.fieldOfView = Mathf.MoveTowards(camera.fieldOfView, normalFieldofView, mouseZ);
            //        }
            //    }
            //    else {
            //        camera.fieldOfView = normalFieldofView;
            //    }

               // camera.fieldOfView = normalFieldofView;
            }
            else {
                transform.position = cameraNormalPosition.position;
                transform.forward = cameraNormalPosition.forward;
                cameraNormalPosition.transform.localEulerAngles = cameraRotation;
            }
        }
    



    public void SetCameraPosition(CameraLocations location){
            switch (location) {
                case CameraLocations.Stand: cameraClosePosition = cameraCloseStandPosition; break;
                case CameraLocations.Crouch: cameraClosePosition = cameraCrouchPosition; break;
                case CameraLocations.Kneel: cameraClosePosition = cameraCloseCrouchPosition; break;
                case CameraLocations.Prone: cameraClosePosition = cameraClosePronePosition; break;
            }
   }
}