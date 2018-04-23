using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {

    public float duration = 2.0f;
    public float fadeStayDuration = 2.0f;
    public Image fadeOutImage;
    public GameObject dialogPanel;
    public GameObject namePanel;
    public GameObject choicePanel;
    public DialogController diagController;
    //public GameObject imageFeedback;

    public void ShowInterface(bool value)
    {
        dialogPanel.SetActive(value);
        namePanel.SetActive(value);
        choicePanel.SetActive(value);
        //imageFeedback.SetActive(value);
    }

    public IEnumerator FadeCanvasGroupAlpha(float startAlpha, float endAlpha)
    {
        ShowInterface(false);
        float elapsedTime = 0f;
        float totalDuration = duration;

        while (elapsedTime < totalDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / totalDuration);
            fadeOutImage.color = new Color(0f,0f,0f, currentAlpha);
            yield return null;
        }

        yield return new WaitForSeconds(fadeStayDuration);

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
        diagController.textAnimator.finished = false;
        ShowInterface(true);
        diagController.SetDialogue();

        
    }
}
