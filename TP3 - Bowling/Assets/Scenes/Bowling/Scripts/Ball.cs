using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float movementSpeed = 10.0f;
    float rollForce = 1000.0f;

    bool applyingForce = false;

    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckInput();

        if (applyingForce)
            rigidBody.AddForce(Vector3.forward * rollForce * Time.deltaTime);

        Debug.Log(applyingForce);
    }

    void CheckInput()
    {
        if (Input.GetButton("Horizontal"))
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            applyingForce = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pine"))
            applyingForce = false;
    }
}
