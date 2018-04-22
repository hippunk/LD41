﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {

    public float duration = 2.0f;
    public Image fadeOutImage;

    public IEnumerator FadeCanvasGroupAlpha(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        float totalDuration = duration;

        while (elapsedTime < totalDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / totalDuration);
            fadeOutImage.color = new Color(0f,0f,0f, currentAlpha);
            yield return null;
        }

        //Fix à l'arache pour défade le putain de fadeOutImage pour changement de scène
        elapsedTime = 0f;
        while (elapsedTime < totalDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentAlpha = Mathf.Lerp(endAlpha, startAlpha, elapsedTime / totalDuration);
            fadeOutImage.color = new Color(0f, 0f, 0f, currentAlpha);
            yield return null;
        }
        fadeOutImage.raycastTarget = false;
    }
}
