using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public string CurrentColor;
    public GameObject MyOwner;
    private string[] _colorList = new string[] { "bleu", "rouge" };

    private void Start()
    {
        CurrentColor = MyOwner.transform.GetComponent<PlayerControler>().CurrentColor;
    }
    private void Update()
    {
        if(CurrentColor == "bleu")transform.GetComponent<Renderer>().material.color = Color.blue;
        if(CurrentColor == "rouge")transform.GetComponent<Renderer>().material.color = Color.red;
    }
    public void SwitchBallColor()
    {
        if (CurrentColor == "bleu") CurrentColor = "rouge";
        else if (CurrentColor == "rouge") CurrentColor = "bleu";
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collide !");
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("player contact !");
            if (CurrentColor != other.transform.GetComponent<PlayerControler>().CurrentColor)
            {
                other.transform.GetComponent<PlayerControler>().Health -= 1;
                Debug.Log(other.transform.GetComponent<PlayerControler>().Health);
            }
            if(!other.transform.GetComponent<PlayerControler>().HandedBall && CurrentColor == other.transform.GetComponent<PlayerControler>().CurrentColor)
            {
                Debug.Log("catch!");
                other.transform.GetComponent<PlayerControler>().HandedBall = true;
                for (int i = 0; i < MyOwner.transform.GetComponent<PlayerControler>().MyBalls.Count; i++)
                {
                    if (MyOwner.transform.GetComponent<PlayerControler>().MyBalls[i] == gameObject)
                    {
                        MyOwner.transform.GetComponent<PlayerControler>().MyBalls.Remove
                                (MyOwner.transform.GetComponent<PlayerControler>().MyBalls[i]);
                    }
                }
                Destroy(gameObject);
            }
        }
        if (other.transform.CompareTag("Wall"))
        {
            Debug.Log("Wall!");
            
        }
    }
}
