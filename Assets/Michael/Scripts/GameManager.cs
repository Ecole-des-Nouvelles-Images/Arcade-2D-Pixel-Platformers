using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { private set; get; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    } 
    
    public void QuitApplication()
    {
        Application.Quit();     
        Debug.Log("à quitté le jeu");
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
