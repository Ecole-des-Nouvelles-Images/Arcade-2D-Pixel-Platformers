using System;
using System.Collections;
using System.Collections.Generic;
using Michael.Scripts.PlayerManager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerSelectionPänel : MonoBehaviour
{
    public GameObject PlayerSelectionPanelPrefab;
    public PlayerInput Input;
    private void Awake()
    {
        var rootMenu = GameObject.Find("MainLayout");
        if (rootMenu != null)
        {
            var menu = Instantiate(PlayerSelectionPanelPrefab, rootMenu.transform);
            Input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<PlayerSelectionController>().SetPlayerIndex(Input.playerIndex);
        }
    }
}
