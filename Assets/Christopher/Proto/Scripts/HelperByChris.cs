using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperByChris
{
    public static void SpriteFliperX(float currentValue, float limite, SpriteRenderer currentSpriteRenderer)//not a dolphin
    {
        if (currentValue < limite)
        {
            currentSpriteRenderer.flipX = true;
        }
        else
        {
            currentSpriteRenderer.flipX = false;
        }
    }
}
