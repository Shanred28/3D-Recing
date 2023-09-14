using UnityEngine;
using UnityEngine.SceneManagement;

namespace Race
{
    public class SceneLoader : MonoBehaviour
    {

        private const string MAINMENUTITLE = "main_menu";
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(MAINMENUTITLE);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

