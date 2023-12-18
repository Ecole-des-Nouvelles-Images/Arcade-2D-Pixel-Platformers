using System;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    [SerializeField] private string sceneToUnload;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private GameObject inputsystem;
    
    [ContextMenu("Switch")]
    public void Switch() {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(sceneToUnload);
        
    }
    
    
    public void SwitchMenu(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
    
    
    
    
    private void Start()
    {
        inputsystem.GetComponent<InputSystemUIInputModule>().enabled = true;
    }
}