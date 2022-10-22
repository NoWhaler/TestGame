using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestGame.UI
{
    public class RestartGame : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}