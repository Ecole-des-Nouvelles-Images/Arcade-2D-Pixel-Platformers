using Michael.Fred;
using UnityEngine;

public class DataModifier : MonoBehaviour {
    
    public void ChangeMusic(AudioClip musicToLoad) 
    { 
        DataManager.Instance.CurrentMusic.clip = musicToLoad;
        DataManager.Instance.CurrentMusic.Play();
    }
    

}