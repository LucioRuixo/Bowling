using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pine : MonoBehaviour
{
    const float minEulerThreshold = 45.0f;
    const float maxEulerThreshold = 315.0f;
    float minHeight = -1;

    [HideInInspector] public bool standing;
    bool fallenXAxis = false;
    bool fallenZAxis = false;

    public PineManager pineManager;

    void Start()
    {
        standing = true;
        pineManager.pines.Add(this);
        Debug.Log(standing);
    }

    void Update()
    {
        CheckIfStanding();
    }

    void CheckIfStanding()
    {
        if (transform.rotation.eulerAngles.x > minEulerThreshold && transform.rotation.eulerAngles.x < maxEulerThreshold)
            fallenXAxis = true;
        else
            fallenXAxis = false;

        if (transform.rotation.eulerAngles.z > minEulerThreshold && transform.rotation.eulerAngles.z < maxEulerThreshold)
            fallenZAxis = true;
        else
            fallenZAxis = false;

        if (fallenXAxis || fallenZAxis)
        {
            if (standing)
            {
                standing = false;
                pineManager.pinesStanding--;
            }
        }
        else
        {
            if (!standing)
            {
                standing = true;
                pineManager.pinesStanding++;
            }
        }
    }

    void CheckHeight()
    {
        if (transform.position.y < minHeight)
        {
            pineManager.pines.Remove(this);
            Destroy(this.gameObject);
        }
    }
}
