using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfigsManager : MonoBehaviour
{
    private List<PlayerConfiguration> _playerConfigs;

    [SerializeField] private int _maxPlayers = 2;
    
    public static PlayerConfigsManager Instance { private set; get; }
    //Singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
        _playerConfigs = new List<PlayerConfiguration>();
    }

    
    public void SetPlayerColor(int index, Material color)
    {
        _playerConfigs[index].PlayerMaterial = color;
    }

    public void PlayerIsReady(int index)
    {
        _playerConfigs[index].IsReady = true;
        if (_playerConfigs.Count == _maxPlayers && _playerConfigs.All( p => p.IsReady))
        {
            GameManager.instance.ChangeScene("Game");
        }
    }
    
}

    public class PlayerConfiguration
    {
        public PlayerConfiguration(PlayerInput pi)
        {
            PlayerIndex = pi.playerIndex;
            Input = pi; 
        }
        public PlayerInput Input {get; set;}
        
        public int PlayerIndex {get; set;}
        
        public bool IsReady {get; set;}
        
        public Material PlayerMaterial {get; set;}
    }
