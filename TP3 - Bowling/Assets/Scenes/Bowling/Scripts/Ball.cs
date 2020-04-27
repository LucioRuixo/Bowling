using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float minLateralPosition;
    float maxLateralPosition;
    float minHeight = -3;
    float minRollForce;
    float maxRollForce;
    float movementSpeed = 10.0f;
    float rollForce = 1000.0f;
    float rollForceStep = 250.0f;

    Vector3 initialPosition;
    Vector3 clampedPosition;

    Quaternion initialRotation;

    bool applyingForce = false;

    public GameObject lane;

    Rigidbody rigidBody;

    public PineManager pineManager;

    void Start()
    {
        minLateralPosition = lane.transform.position.x - (lane.transform.localScale.x / 2) * 10;
        maxLateralPosition = lane.transform.position.x + (lane.transform.localScale.x / 2) * 10;
        minRollForce = rollForce - rollForceStep * 2;
        maxRollForce = rollForce + rollForceStep * 2;

        initialPosition = transform.position;
        initialRotation = transform.rotation;

        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckInput();

        if (applyingForce)
            rigidBody.AddForce(Vector3.forward * rollForce * Time.deltaTime);

        ClampLateralPosition();
        CheckHeight();
    }

    void CheckInput()
    {
        if (Input.GetButton("Horizontal"))
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Vertical"))
            rollForce = Mathf.Clamp(rollForce + rollForceStep * Input.GetAxisRaw("Vertical"), minRollForce, maxRollForce);

        if (Input.GetKeyDown(KeyCode.Space))
            applyingForce = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pine"))
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

    void CheckHeight()
    {
        if (transform.position.y < minHeight)
        {
            applyingForce = false;
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            pineManager.removeFallenPines = true;
        }
    }
}
