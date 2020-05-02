using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Pin : MonoBehaviour
    {
        public float maxInclinationAngle;
        float lowerInclinationLimit;
        float higherInclinationLimit;
        float minHeight;

        bool fallenXAxis;
        bool fallenZAxis;

        public GameManager gameManager;

        void Start()
        {
            maxInclinationAngle = 25f;
            lowerInclinationLimit = maxInclinationAngle;
            higherInclinationLimit = 360f - maxInclinationAngle;
            minHeight = -2f;

            fallenXAxis = false;
            fallenZAxis = false;

            gameManager.pins.Add(this);
        }

        public bool BelowMinHeight()
        {
            return transform.position.y < minHeight;
        }

        public bool Fallen()
        {
            if (transform.rotation.eulerAngles.x > lowerInclinationLimit && transform.rotation.eulerAngles.x < higherInclinationLimit)
                fallenXAxis = true;
            else
                fallenXAxis = false;

            if (transform.rotation.eulerAngles.z > lowerInclinationLimit && transform.rotation.eulerAngles.z < higherInclinationLimit)
                fallenZAxis = true;
            else
                fallenZAxis = false;

            return fallenXAxis || fallenZAxis;
        }
    }
}