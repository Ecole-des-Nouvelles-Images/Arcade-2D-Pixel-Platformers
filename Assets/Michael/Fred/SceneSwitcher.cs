using System;
using Michael.Fred;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;


public class SceneSwitcher : MonoBehaviour
{
    //TODO Refacto
    [SerializeField] private string sceneToUnload;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private GameObject inputsystem;
    
    [ContextMenu("Switch")]
    public void Switch() {
        CustomSceneManager.Instance.LoadScene(sceneToLoad);
    }
    
    
    public void SwitchMenu(string sceneName)
    {
        TimeManager.Instance.timeScale = 1;
        CustomSceneManager.Instance.LoadScene(sceneName);
    }
    
    
    
    
    private void Awake()
    {
      
        inputsystem.GetComponent<InputSystemUIInputModule>().enabled = true;
        DataManager.Instance.playMusic();
    }
}