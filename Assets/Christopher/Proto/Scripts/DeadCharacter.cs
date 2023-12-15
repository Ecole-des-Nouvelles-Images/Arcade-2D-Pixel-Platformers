using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCharacter : MonoBehaviour
{
    public AnimatorOverrideController CurrentAnimator;

    [SerializeField] private Material DeathFXmaterial;
    [SerializeField] private GameObject DeathPrefab;

    private void DoDeathFX()
    {
        var o = Instantiate(DeathPrefab);
        o.transform.position = transform.position;
        o.GetComponent<SpriteRenderer>().material = DeathFXmaterial;
        o.GetComponent<Animator>().runtimeAnimatorController = CurrentAnimator;
    }
}
