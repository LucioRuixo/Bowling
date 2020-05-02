using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Ball : MonoBehaviour
    {
        public float rollForce;
        float minLateralPosition;
        float maxLateralPosition;
        float minRollForce;
        float maxRollForce;
        float minHeight;
        float movementSpeed;
        float rollForceStep;

        bool applyingForce;
        bool rolling;

        Vector3 initialPosition;
        Vector3 clampedPosition;

        Quaternion initialRotation;

        public GameObject lane;

        Rigidbody rigidBody;

        public GameManager gameManager;

        void Start()
        {
            rollForce = 1000f;
            movementSpeed = 10f;
            rollForceStep = 250f;
            minLateralPosition = lane.transform.position.x - (lane.transform.localScale.x / 2f) * 10f;
            maxLateralPosition = lane.transform.position.x + (lane.transform.localScale.x / 2f) * 10f;
            minRollForce = rollForce - rollForceStep * 2f;
            maxRollForce = rollForce + rollForceStep * 2f;
            minHeight = -3f;

            applyingForce = false;
            rolling = false;

            initialPosition = transform.position;
            initialRotation = transform.rotation;

            rigidBody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (!gameManager.gameEnded)
                CheckInput();

            if (applyingForce)
                rigidBody.AddForce(Vector3.forward * rollForce * Time.deltaTime);
            
            if (transform.localPosition.z == initialPosition.z)
                ClampLateralPosition();
        }

        void CheckInput()
        {
            if (Input.GetButton("Horizontal") && !rolling)
                transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Vertical"))
                rollForce = Mathf.Clamp(rollForce + rollForceStep * Input.GetAxisRaw("Vertical"), minRollForce, maxRollForce);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                applyingForce = true;
                rolling = true;
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Pin"))
                applyingForce = false;
        }

        void ClampLateralPosition()
        {
            if (transform.position.x < minLateralPosition || transform.position.x > maxLateralPosition)
            {
                clampedPosition = transform.position;
                clampedPosition.x = Mathf.Clamp(clampedPosition.x, minLateralPosition, maxLateralPosition);
                transform.position = clampedPosition;
            }
        }

        public bool BelowMinHeight()
        {
            return transform.position.y < minHeight;
        }

        public void Reset()
        {
            applyingForce = false;
            rolling = false;
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            transform.position = initialPosition;
            transform.rotation = initialRotation;
        }
    }
}
