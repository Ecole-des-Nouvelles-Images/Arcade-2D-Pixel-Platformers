using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Michael.Scripts
{
  public class Changecolor : MonoBehaviour

  {
    public static Action OnChangeColor;
    [SerializeField] private List<Material> playerShader;
    [SerializeField] private GameObject playerArmor;
    [SerializeField] private bool isRed;
    [SerializeField] private bool isBlue;

    private void OnEnable()
    {
      OnChangeColor += ChangeColor;
    }

    private void OnDisable()
    {
      OnChangeColor -= ChangeColor;
    }


    public void ChangeColor()
    {
      gameObject.GetComponent<SpriteRenderer>().material = playerShader[0];
    }

    public void TargetChoice()
    {
      
    }
    
  }
}
