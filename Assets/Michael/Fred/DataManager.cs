using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Michael.Fred
{
    
    public class DataManager : MonoBehaviourSingleton<DataManager> {

        public string StartingScene;
        public AudioSource CurrentMusic;
        public Dictionary<int, int> PlayerDatasDict = new Dictionary<int, int>();
        // public List<PlayerData> PlayerDatasList;
    
    
        private void Start()
        {
            SceneManager.LoadScene(StartingScene, LoadSceneMode.Additive);
        
        }

        


       



    }
}