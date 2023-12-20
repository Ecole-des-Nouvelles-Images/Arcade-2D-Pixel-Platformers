using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Michael.Fred
{
    
    public class DataManager : MonoBehaviourSingleton<DataManager> {

        public string StartingScene;
        public AudioSource CurrentMusic;
        public Dictionary<int, int> PlayerDatasDict = new Dictionary<int, int>();
        public AudioMixer AudioMixer;
        public float SfxVolume;
        public float MusicVolume;
        
        public void StopMusic()
        {
            if (CurrentMusic)
            {
                CurrentMusic.Stop();
            }
         
        }
       


       



    }
}