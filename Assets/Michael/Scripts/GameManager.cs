using System;
using System.Collections;
using System.Collections.Generic;
using Michael.Fred;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public static int CurrentRound;
    public List<GameObject> PlayerList;
    [SerializeField] public UnityEvent ChangeMusic;
    public void QuitApplication()
    {
        Application.Quit();     
        Debug.Log("à quitté le jeu");
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    private void Start()
    {
        ChangeMusic.Invoke();
    }
    
    
    

  
    
    
}
