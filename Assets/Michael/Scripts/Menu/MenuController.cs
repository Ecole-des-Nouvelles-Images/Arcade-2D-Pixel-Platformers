using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Michael.Scripts
    {
        public enum PanelType
        {
            None, 
            Main,
            Options,
            Credits,
        }
        public class MenuController : MonoBehaviour
        {
            [SerializeField] private List<MenuPanel> _panelList;

            private Dictionary<PanelType, MenuPanel> _panelsDict; 


            private void Start()
            {
               
            }

            public void OpenPanel(PanelType type)
            {
                
            }

            private void OpenOnePanel(PanelType type)
            {
                
            }
            
            
            
            
        }
    }
