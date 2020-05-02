using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class UIManager : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
