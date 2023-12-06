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
        if (other.transform.CompareTag("Player"))
        {
            if (CurrentColor != other.transform.GetComponent<PlayerControler>().CurrentColor)
            {
                other.transform.GetComponent<Rigidbody2D>().velocity += transform.GetComponent<Rigidbody2D>().velocity;
                transform.GetComponent<Rigidbody2D>().velocity *= -1;
                other.transform.GetComponent<PlayerControler>().Health -= 1;
                Debug.Log(other.transform.GetComponent<PlayerControler>().Health);
            }
            if(!other.transform.GetComponent<PlayerControler>().HandedBall && CurrentColor == other.transform.GetComponent<PlayerControler>().CurrentColor)
            {
                other.transform.GetComponent<PlayerControler>().HandedBall = true;
                for (int i = 0; i < MyOwner.transform.GetComponent<PlayerControler>().MyBalls.Count; i++)
                {
                    if (MyOwner.transform.GetComponent<PlayerControler>().MyBalls[i] == gameObject)
                    {
                        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        MyOwner.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
