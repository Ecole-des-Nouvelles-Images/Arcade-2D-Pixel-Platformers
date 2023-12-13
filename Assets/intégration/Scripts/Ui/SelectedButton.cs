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
    
    void Update()
    {
        if (_eventSystem.currentSelectedGameObject == _selectedButton)
        {
            _selectedButton.GetComponent<Animator>().SetBool("IsSelected",true);
            _selectedOutline.GetComponent<Animator>().SetBool("IsSelected",true);
            _selectedButton.GetComponent<Button>().interactable = true;
            _selectedOutline.SetActive(true);
            
        }
        else
        {
            _selectedButton.GetComponent<Animator>().SetBool("IsSelected",false);
            _selectedOutline.GetComponent<Animator>().SetBool("IsSelected",false);

            _selectedOutline.SetActive(false);
            _selectedButton.GetComponent<Button>().interactable = false;
           
        }
    }
}
