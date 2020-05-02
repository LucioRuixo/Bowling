using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class MainCamera : MonoBehaviour
    {
        public float maxTravellingPosition;

        public Vector3 ballOffset;
        public Vector3 canvasOffset;

        public GameManager gameManager;

        public Transform ballTransform;

        public RectTransform canvasTransform;

        void Update()
        {
            if (!gameManager.gameEnded)
            {
                if (transform.position.z < maxTravellingPosition)
                    transform.position = ballTransform.position + ballOffset;
            }
            else
                transform.position = canvasTransform.position + canvasOffset;
        }
    }
}
