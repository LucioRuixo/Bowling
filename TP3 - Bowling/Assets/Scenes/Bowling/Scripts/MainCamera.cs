using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Vector3 offset;

    public GameObject pivot;

    void Update()
    {
        transform.position = pivot.transform.position + offset;
    }
}
