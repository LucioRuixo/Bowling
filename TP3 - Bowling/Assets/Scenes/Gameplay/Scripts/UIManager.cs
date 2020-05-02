using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class UIManager : MonoBehaviour
    {
        int pinsLeft;
        int shotsLeft;

        float force;

        bool enableEndgameScreen;

        public Ball ball;
        public GameManager gameManager;

        void Start()
        {
            enableEndgameScreen = true;
        }

        void Update()
        {
            if (!gameManager.gameEnded)
            {
                foreach (Text text in GetComponentsInChildren<Text>())
                {
                    switch (text.gameObject.name)
                    {
                        case "Force":
                            force = ball.rollForce;
                            text.text = "Force: " + force;
                            break;
                        case "Pins Left":
                            pinsLeft = gameManager.pinsStanding;
                            text.text = "Pins left: " + pinsLeft;
                            break;
                        case "Shots Left":
                            shotsLeft = gameManager.shotsLeft;
                            text.text = "Shots left: " + shotsLeft;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                if (enableEndgameScreen)
                {
                    foreach (Transform child in GetComponentsInChildren(typeof(Transform), true))
                    {
                        switch (child.name)
                        {
                            case "Force":
                            case "Pins Left":
                            case "Shots Left":
                                child.gameObject.SetActive(false);
                                break;
                            case "Endgame Background":
                            case "Endgame Text":
                            case "Return To Main Menu":
                                child.gameObject.SetActive(true);
                                break;
                            default:
                                break;
                        }
                    }

                    enableEndgameScreen = false;
                }

                foreach (Text text in GetComponentsInChildren<Text>())
                {
                    switch (text.gameObject.name)
                    {
                        case "Endgame Text":
                            if (gameManager.playerWon)
                                text.text = "YOU WON!";
                            else
                                text.text = "YOU LOST :(";
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}