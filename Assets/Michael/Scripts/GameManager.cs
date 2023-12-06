using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public void QuitApplication()
    {
        Application.Quit();     
        Debug.Log("à quitté le jeu");
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    
    public void OpenCharacterSelection()
    {
        GameManager.Instance.ChangeScene("CharacterSelection");
    }
    
    
    
    
}
