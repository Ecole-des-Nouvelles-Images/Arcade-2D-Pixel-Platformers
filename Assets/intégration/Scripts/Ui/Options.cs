using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
   public void  FullScreenTogle()
   {
      Screen.fullScreen = !Screen.fullScreen;
   }
   
   public void  Quitapplication()
   {
      Application.Quit();
   }
}
