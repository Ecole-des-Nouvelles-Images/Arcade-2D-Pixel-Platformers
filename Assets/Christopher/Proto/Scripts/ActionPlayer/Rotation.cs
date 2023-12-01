using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public void LookAt(Vector2 _mouvementValue,Transform _visé)
    {
        //visé en mode gros dégueulasse
        if (_mouvementValue.x > 0.1f && _mouvementValue.y > 0.1f) //diag haut droit
        {
            _visé.position = new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z);
        } 
        if (_mouvementValue.x < -0.1f && _mouvementValue.y < -0.1f)//diag bas gauche
            _visé.position = new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z);
        if (_mouvementValue.x < -0.1f && _mouvementValue.y > 0.1f)//diag haut gauche
            _visé.position = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z);
        if (_mouvementValue.x > 0.1f && _mouvementValue.y < -0.1f)//diag bas droit
            _visé.position = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z);
        if(_mouvementValue == Vector2.zero)// par défaut : droit
            _visé.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        if (_mouvementValue.y < 0.1 && _mouvementValue.y > -0.1 && _mouvementValue.x > 0.1f )//droit
            _visé.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        if (_mouvementValue.y < 0.1 && _mouvementValue.y > -0.1 && _mouvementValue.x < -0.1f )//gauche
            _visé.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        if (_mouvementValue.x < 0.1 && _mouvementValue.x > -0.1 && _mouvementValue.y > 0.1f)//haut
            _visé.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        if (_mouvementValue.x < 0.1 && _mouvementValue.x > -0.1 && _mouvementValue.y < -0.1f)//bas
            _visé.position = new Vector3(transform.position.x , transform.position.y - 1, transform.position.z);
    }
}
