using System;
using System.Collections;
using System.Collections.Generic;
using Christopher.Proto.Scripts;
using Michael.Scripts;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 5;
    [SerializeField] private int minLimitMove = -10;
    [SerializeField] private int maxLimitMove = 10;
    [SerializeField] private bool isTrigger;
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
            transform.Translate(Vector3.right * (moveSpeed * TimeManager.Instance.deltaTime));
        }
        else if (_goForward && transform.position.x >= maxLimitMove)
        {
            transform.Translate(Vector3.left * (moveSpeed * TimeManager.Instance.deltaTime));
            _goForward = false;
        }
        if (!_goForward && transform.position.x > minLimitMove)
        {
            transform.Translate(Vector3.left * (moveSpeed * TimeManager.Instance.deltaTime));
        }
        else if (!_goForward && transform.position.x <= minLimitMove)
        {
            transform.Translate(Vector3.right * (moveSpeed * TimeManager.Instance.deltaTime));
            _goForward = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            isTrigger = true;
            other.transform.GetComponent<PlayerData>().PlayHurtSound();
            other.transform.GetComponent<PlayerData>().Health = 0;
        }
    }
   /* private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.transform.CompareTag("Player"))
        {
            isTrigger = true;
            other.transform.GetComponent<PlayerData>().Health -= 1;
        }
    }*/
}
