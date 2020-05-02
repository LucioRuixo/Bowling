using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [HideInInspector] public int pinsStanding;
        [HideInInspector] public int shotsLeft;

        [HideInInspector] public bool playerWon;
        [HideInInspector] public bool gameEnded;

        public Ball ball;
        public MainCamera mainCamera;
        public UIManager uIManager;

        [HideInInspector] public List<Pin> pins = new List<Pin>();

        void Start()
        {
            pinsStanding = 10;
            shotsLeft = 3;

            gameEnded = false;
        }

        void Update()
        {
            CheckPinsHeight();

            if (ball.BelowMinHeight())
            {
                ResetShot();

                if (pinsStanding == 0)
                {
                    playerWon = true;
                    gameEnded = true;
                }

                if (!gameEnded && shotsLeft == 0)
                {
                    playerWon = false;
                    gameEnded = true;
                }
            }
        }

        void CheckPinsHeight()
        {
            foreach (Pin pin in pins.ToArray())
            {
                if (pin.BelowMinHeight())
                {
                    pins.Remove(pin);
                    Destroy(pin.gameObject);
                }
            }

            pinsStanding = pins.Count;
        }

        void RemoveFallenPins()
        {
            foreach (Pin pin in pins.ToArray())
            {
                if (pin.Fallen())
                {
                    pins.Remove(pin);
                    Destroy(pin.gameObject);
                }
            }

            pinsStanding = pins.Count;
        }

        void ResetShot()
        {
            ball.Reset();

            mainCamera.transform.position = ball.transform.position + mainCamera.ballOffset;

            RemoveFallenPins();

            shotsLeft--;
        }
    }
}