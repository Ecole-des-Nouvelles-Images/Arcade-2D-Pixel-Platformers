using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    [SerializeField] private string sceneToUnload;
    [SerializeField] private string sceneToLoad;
    
    [ContextMenu("Switch")]
    public void Switch() {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(sceneToUnload);
    }
    
}