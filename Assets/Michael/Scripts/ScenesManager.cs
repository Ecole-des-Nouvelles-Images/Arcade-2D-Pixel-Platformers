using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Michael.Scripts
{
    public class ScenesManager : MonoBehaviour
    {

        public void QuitApplication()
        {
            Application.Quit();
            Debug.Log("à quitté le jeu");
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Prototype game");
        }
        
        
        
        
        
        
        
    }
}
