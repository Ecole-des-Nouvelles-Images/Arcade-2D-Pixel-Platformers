using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Deplacement : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = transform.GetComponent<Rigidbody2D>();
    }
    public void PerformMovement(Vector2 move,float speed)
    {
        float Xmove = move.x * speed * Time.deltaTime;//* -1;
        float Zmove = move.y * speed * Time.deltaTime;
        move = new Vector2(Xmove, Zmove);
        _rb.AddForce(move, ForceMode2D.Impulse);
    }
    
}
