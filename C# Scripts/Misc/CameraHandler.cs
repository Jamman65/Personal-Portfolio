using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{

    public class CameraHandler : MonoBehaviour
    {
        InputHandler inputhandler;
        PlayerManager playermanager;

        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform targetTransformWhileAiming;
        public Transform cameraPivotTransform;
        private Transform myTransform;
        private Vector3 cameraTransformPosition;
        public LayerMask ignoreLayers;
        public LayerMask environmentLayer;
        private Vector3 cameraFollowVelocity = Vector3.zero;
        public Camera cameraobject;

        public static CameraHandler singelton;

        public float LeftandRightSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float UpandDownSpeed = 0.03f;
        public float LeftandRightAimingSpeed;
        public float UpandDownAimingSpeed;

        private float targetposition;
        private float defaultPosition;
        private float LeftandRightAngle;
        private float UpandDownAngle;
        public float minimumPivot = -35;
        public float maximumPivot = 35;

        public float camerasphereRadius = 0.2f;
        public float cameraCollisonOffset = 0.2f;
        public float minimumCollisionOffset = 0.2f;
        public float lockedPivotPosition = 2.25f;
        public float unlockedPivotPosition = 1.65f;

        public Transform currentLockOnTarget;

        List<CharacterManager> availableTargets = new List<CharacterManager>();
        public Transform nearestLockOnTarget;
        public Transform leftLockTarget;
        public Transform RightLockTarget;
        public float MaximumLockOnDistance = 30;


        private void Awake()
        {
            singelton = this;
            myTransform = transform;
            defaultPosition = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
            inputhandler = FindObjectOfType<InputHandler>();
            playermanager = FindObjectOfType<PlayerManager>();
            cameraobject = GetComponentInChildren<Camera>();

        }

        private void Start()
        {
            environmentLayer = LayerMask.NameToLayer("Environment");
        }

        public void FollowTarget(float delta)
        {
            if (playermanager.isAimingArrow)
            {
                Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransformWhileAiming.position, ref cameraFollowVelocity, delta + followSpeed);
                transform.position = targetPosition;
            }
            else
            {
                Vector3 targetPosition = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVelocity, delta / followSpeed);
                myTransform.position = targetPosition;
                
            }
            HandleCameraCollisions(delta);



        }

        public void HandleCameraRotation()
        {
            if(inputhandler.lockOnFlag && currentLockOnTarget != null)
            {
                HandleLockedCameraRotation();
            }
            else if (playermanager.isAimingArrow)
            {
                HandleAimedCameraRotation();
            }
            else
            {
                HandleStandardCameraRotation();
            }
        }

        public void HandleLockedCameraRotation()
        {
            // this code forces the camera to rotate towards the object that the player has locked onto
            //float velocity = 0;
            Vector3 dir = currentLockOnTarget.position - transform.position;
            dir.Normalize();
            dir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = targetRotation;

            dir = currentLockOnTarget.position - cameraPivotTransform.position;
            dir.Normalize();

            targetRotation = Quaternion.LookRotation(dir);
            Vector3 eulerAngle = targetRotation.eulerAngles;
            eulerAngle.y = 0;
            cameraPivotTransform.localEulerAngles = eulerAngle;

        }

        public void HandleAimedCameraRotation()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            cameraPivotTransform.rotation = Quaternion.Euler(0, 0, 0);

            Quaternion targetRotationX;
            Quaternion targetRotationY;

            Vector3 cameraRotationX = Vector3.zero;
            Vector3 cameraRotationY = Vector3.zero;

            LeftandRightAngle += (inputhandler.MouseX * LeftandRightAimingSpeed) * Time.deltaTime;
            UpandDownAngle -= (inputhandler.MouseY * UpandDownAimingSpeed) * Time.deltaTime;

            cameraRotationY.y = LeftandRightAngle;
            targetRotationY = Quaternion.Euler(cameraRotationY);
            targetRotationX = Quaternion.Slerp(transform.rotation, targetRotationY, 1);
            transform.localRotation = targetRotationY;

            cameraRotationX.x = UpandDownAngle;
            targetRotationX = Quaternion.Euler(cameraRotationX);
            targetRotationX = Quaternion.Slerp(cameraTransform.localRotation, targetRotationX, 1);
            cameraTransform.localRotation = targetRotationX;
        }

        public void ResetAimCameraRotation()
        {
            cameraTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        public void HandleStandardCameraRotation()
        {

            LeftandRightAngle += inputhandler.MouseX * LeftandRightSpeed * Time.deltaTime;
            UpandDownAngle -= inputhandler.MouseY * UpandDownSpeed * Time.deltaTime;
            UpandDownAngle = Mathf.Clamp(UpandDownAngle, minimumPivot, maximumPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = LeftandRightAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            myTransform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = UpandDownAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;


        }

        private void HandleCameraCollisions(float delta)
        {
            targetposition = defaultPosition;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
            direction.Normalize();

            if(Physics.SphereCast(cameraPivotTransform.position, camerasphereRadius, direction, out hit, Mathf.Abs(targetposition), ignoreLayers))
            {
                float distance = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetposition = -(distance - cameraCollisonOffset);
            }

            if(Mathf.Abs(targetposition) < minimumCollisionOffset)
            {
                targetposition = -minimumCollisionOffset;
            }

            cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetposition, delta / 0.2f);
            cameraTransform.localPosition = cameraTransformPosition;
        }

        public void HandleLockOn()
        {
            // This code searches for objects with the character manager script within a certain radius
            float shortestDistance = Mathf.Infinity;
            float shortestDistanceFromLeftTarget = Mathf.Infinity;
            float shortestDistanceFromRightTarget = Mathf.Infinity;

            Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26);

            for(int i = 0; i < colliders.Length; i++)
            {
                CharacterManager character = colliders[i].GetComponent<CharacterManager>();

                if (character != null)
                {
                    Vector3 lockTargetDirection = character.transform.position - targetTransform.position;
                    float distanceFromTarget = Vector3.Distance(targetTransform.position, character.transform.position);
                    // Only allows for the player to lock onto objects in their view
                    float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward);
                    RaycastHit hit;

                    // This stops the player from being able to lock onto itself
                    if (character.transform.root != targetTransform.transform.root && viewableAngle > -50 &&
                        viewableAngle < 50 && distanceFromTarget <= MaximumLockOnDistance)
                    {
                        if(Physics.Linecast(playermanager.lockOnTransform.position, character.transform.position, out hit))
                        {
                            Debug.DrawLine(playermanager.lockOnTransform.position, character.transform.position);

                            if(hit.transform.gameObject.layer == environmentLayer)
                            {
                                //player cant lock on since there is an object in the way of an enemy
                                Debug.Log("there is an object in the way");

                            }

                            else
                            {
                                availableTargets.Add(character);
                            }
                        }
                       
                    }


                }
            }

            for(int k = 0; k < availableTargets.Count; k++)
            {
                float distanceFromTarget = Vector3.Distance(targetTransform.position, availableTargets[k].transform.position);

                if(distanceFromTarget < shortestDistance)
                {
                    shortestDistance = distanceFromTarget;
                    nearestLockOnTarget = availableTargets[k].lockOnTransform;
                }

                if (inputhandler.lockOnFlag)
                {
                    Vector3 EnemyPosition = currentLockOnTarget.InverseTransformPoint(availableTargets[k].transform.position);
                    var distanceFromLeftTarget = currentLockOnTarget.transform.position.x + availableTargets[k].transform.position.x;
                    var distanceFromRightTarget = currentLockOnTarget.transform.position.x - availableTargets[k].transform.position.x;

                    if(EnemyPosition.x > 0.00 && distanceFromLeftTarget < shortestDistanceFromLeftTarget)
                    {
                        shortestDistanceFromLeftTarget = distanceFromLeftTarget;
                        leftLockTarget = availableTargets[k].lockOnTransform;
                    }

                    if(EnemyPosition.x < 0.00 && distanceFromRightTarget < shortestDistanceFromRightTarget)
                    {
                        shortestDistanceFromRightTarget = distanceFromRightTarget;
                        RightLockTarget = availableTargets[k].lockOnTransform;
                    }
                }
            }
        }

        public void ClearLockOnTargets()
        {
            availableTargets.Clear();
            nearestLockOnTarget = null;
            currentLockOnTarget = null;
        }

        public void SetCameraHeight()
        {
            Vector3 velocity = Vector3.zero;
            Vector3 newLockedPosition = new Vector3(0, lockedPivotPosition);
            Vector3 newUnlockedPosition = new Vector3(0, unlockedPivotPosition);

            //These functions adjust the cameras height when the player is locked onto an object and when they are not locked on
            if(currentLockOnTarget != null)
            {
                cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newLockedPosition, ref velocity, Time.deltaTime);

            }

            else
            {
                cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newUnlockedPosition, ref velocity, Time.deltaTime);
            }
        }


    }
}
