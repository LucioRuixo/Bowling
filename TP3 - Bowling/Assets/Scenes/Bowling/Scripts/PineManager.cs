using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineManager : MonoBehaviour
{
    [HideInInspector] public int pinesStanding = 10;

    public bool removeFallenPines = false;

    [HideInInspector] public List<Pine> pines = new List<Pine>();

    void Update()
    {
        if (removeFallenPines)
            RemoveFallenPines();
        Debug.Log(pinesStanding);
    }

    void RemoveFallenPines()
    {
        foreach (Pine pine in pines.ToArray())
        {
            if (!pine.standing)
            {
                pines.Remove(pine);
                Destroy(pine.gameObject);
            }
        }

        removeFallenPines = false;
    }
}
