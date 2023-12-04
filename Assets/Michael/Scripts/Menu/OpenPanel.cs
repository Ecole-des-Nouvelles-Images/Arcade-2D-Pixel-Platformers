using System.Collections;
using System.Collections.Generic;
using Michael.Scripts;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    [SerializeField] private PanelType _type;

    [SerializeField] private MenuController _menuController;
    
    void Start()
    {
      
    }

    public void onButtonDown()
    {
        _menuController.OpenPanel(_type);
    }

    
    
}
