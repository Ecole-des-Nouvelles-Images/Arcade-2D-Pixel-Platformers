using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLazerManager : MonoBehaviour
{
    public GameObject DeathLazer;
    public AudioSource HornSound;
  
    

    public void ShowDeathLazer()
    {
        DeathLazer.SetActive(true);
    }
    
    public void PlayHornsound()
    {
        HornSound.Play();
    }

    public void PlayersMovement()
    {
        CountDownController.CanPlay = true;
    }
   
}
