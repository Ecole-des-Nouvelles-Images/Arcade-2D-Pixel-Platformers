using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRecover : MonoBehaviour
{
    public bool isRecovering;

    [SerializeField] private float recoveringTimer = 2f;
    [SerializeField] private float flikerTimer = 0.2f;

    private float _currentRecoveringTime;
    private float _currentFlikerTime;
    private SpriteRenderer _characterSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _characterSpriteRenderer = transform.GetComponent<SpriteRenderer>();
        _currentRecoveringTime = recoveringTimer;
    }

    // Update is called once per frame
    void Update()
    {
        Recovering();
    }

    private void Recovering()
    {
        if (isRecovering && _currentRecoveringTime > 0)
        {
            _currentRecoveringTime -= TimeManager.Instance.deltaTime;
            FlikingSprite();
        }

        if (_currentRecoveringTime <= 0)
        {
            if (!_characterSpriteRenderer.enabled)
            {
                _characterSpriteRenderer.enabled = true;
            }
            isRecovering = false;
            _currentFlikerTime = flikerTimer;
            _currentRecoveringTime = recoveringTimer;
        }
    }

    private void FlikingSprite()
    {
        if ( _currentFlikerTime > 0)
        {
            _currentFlikerTime -= Time.deltaTime;
        }
        if (_currentFlikerTime <= 0)
        {
            _characterSpriteRenderer.enabled = !_characterSpriteRenderer.enabled;
            _currentFlikerTime = flikerTimer;
        }
        
    }
}
