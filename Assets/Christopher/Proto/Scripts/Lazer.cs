using System;
using System.Collections;
using System.Collections.Generic;
using Christopher.Proto.Scripts;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 5;
    [SerializeField] private int minLimitMove = -10;
    [SerializeField] private int maxLimitMove = 10;
    private bool _isTrigger;
    private bool _goForward = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_goForward && transform.position.x < maxLimitMove)
        {
            transform.Translate(Vector3.right * (moveSpeed * Time.deltaTime));
        }
        else if (_goForward && transform.position.x >= maxLimitMove)
        {
            transform.Translate(Vector3.left * (moveSpeed * Time.deltaTime));
            _goForward = false;
        }
        if (!_goForward && transform.position.x > minLimitMove)
        {
            transform.Translate(Vector3.left * (moveSpeed * Time.deltaTime));
        }
        else if (!_goForward && transform.position.x <= minLimitMove)
        {
            transform.Translate(Vector3.right * (moveSpeed * Time.deltaTime));
            _goForward = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.transform.CompareTag("Player"))
        {
            _isTrigger = true;
            other.transform.GetComponent<PlayerData>().Health -= 1;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.transform.CompareTag("Player"))
        {
            _isTrigger = true;
            other.transform.GetComponent<PlayerData>().Health -= 1;
        }
    }
}
