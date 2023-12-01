using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public string CurrentColor;
    private string[] _colorList = new string[] { "bleu", "rouge" };

    private void Start()
    {
        int randomColorIndex = Random.Range(0, _colorList.Length);
        CurrentColor = _colorList[randomColorIndex];
    }

    private void Update()
    {
        if(CurrentColor == "bleu")transform.GetComponent<Renderer>().material.color = Color.blue;
        if(CurrentColor == "rouge")transform.GetComponent<Renderer>().material.color = Color.red;
    }

    public void SwitchBallColor()
    {
        for (int i = 0; i < _colorList.Length - 1; i++)
        {
            if (CurrentColor == _colorList[i])
            {
                if (i == _colorList.Length - 1) CurrentColor = _colorList[0];
                else CurrentColor = _colorList[i + 1];
            }
            
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (CurrentColor != other.transform.GetComponent<PlayerControler>().CurrentColor)
            {
                other.transform.GetComponent<PlayerControler>().Health -= 1;
            }
            else if(!other.transform.GetComponent<PlayerControler>().HandedBall)
            {
                other.transform.GetComponent<PlayerControler>().HandedBall = true;
                Destroy(gameObject);
            }
        }
    }
}
