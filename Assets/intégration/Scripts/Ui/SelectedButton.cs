using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{
    [SerializeField] private GameObject _selectedButton;
    [SerializeField] private GameObject _selectedOutline;
    [SerializeField] private EventSystem _eventSystem;
    private Animator _buttonAnimator;
    private Animator _SelectionAnimator;
    


    private void Start()
    {
        _buttonAnimator = _selectedButton.GetComponent<Animator>();
        _SelectionAnimator = _selectedOutline.GetComponent<Animator>();
    }

    void Update()
    {
        if (_eventSystem.currentSelectedGameObject == _selectedButton) {
           // _selectedButton.GetComponent<Button>().interactable = true;
            _selectedOutline.GetComponent<Image>().enabled = true;
            _buttonAnimator.SetBool("IsSelected",true);
            _SelectionAnimator.SetBool("IsSelected",true);
            
            
        }
        else {
            _selectedOutline.GetComponent<Image>().enabled = false;
           // _selectedButton.GetComponent<Button>().interactable = false;
            _buttonAnimator.SetBool("IsSelected",false);
           _SelectionAnimator.SetBool("IsSelected",false);
        }
    }


    public void LoadScene(string SceneToLoad)
    {
        CustomSceneManager.Instance.LoadScene(SceneToLoad);
    }
    
    public void ReloadScene()
    {
        CustomSceneManager.Instance.ReloadActiveScene();
    }

  
    
    
}
