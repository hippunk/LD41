using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionsController : MonoBehaviour {

    [SerializeField] private GameObject gameObjectAnger;
    [SerializeField] private GameObject gameObjectJoy;
    [SerializeField] private GameObject gameObjectDisgust;
    [SerializeField] private GameObject gameObjectEmbarrassment;
    [SerializeField] private GameObject gameObjectSadness;

    private Emotion savedEmotion;
    private GameObject displayedEmotion;
    private SpriteRenderer displayedImageEmotion;
    private bool isAnimFinished = false;

    // Use this for initialization
    void Start() {
        savedEmotion = GameLogicManager.currentCharacter.emotion;
    }

    // Update is called once per frame
    void FixedUpdate() {
        var currentEmotion = GameLogicManager.currentCharacter.emotion;
        if (this.savedEmotion != currentEmotion) {
            this.savedEmotion = currentEmotion;
            if (this.savedEmotion != Emotion.neutral && this.savedEmotion != Emotion.none) {
                this.SetDisplayedEmotion(this.savedEmotion);
                this.StartCoroutine(this.TriggerEmotion(this.savedEmotion));
                this.TriggerEmotion(this.savedEmotion);
            }
        }
    }

    IEnumerator TriggerEmotion(Emotion emotion) {
        while (!isAnimFinished) {
            this.isAnimFinished = this.AnimateEmotion();
            yield return new WaitForSeconds(.1f);
        }
        this.ResetDisplayedEmotion();
    }

    private bool AnimateEmotion() {
        // elevate image
        var position = this.displayedEmotion.transform.position;
        Vector3 newPosition = new Vector3(position.x, position.y + .1f, position.z);
        this.displayedEmotion.transform.position = newPosition;

        // fade image
        var color = this.displayedImageEmotion.color;
        Color newColor = new Color { r = 1, g = 1, b = 1, a = color.a - .1f };
        this.displayedImageEmotion.color = newColor;

        if (newPosition.y >= 7) {
            return true;
        }
        return false;
    }

    private void ResetDisplayedEmotion() {
        this.displayedEmotion.SetActive(false);
        this.displayedEmotion.transform.position = new Vector3 { x = 3.5f, y = 6, z = 100 };
        this.displayedImageEmotion.color = new Color { r = 1, g = 1, b = 1, a = 1 };
        this.isAnimFinished = false;
    }

    private void SetDisplayedEmotion(Emotion emotion) {
        switch (emotion) {
            case Emotion.anger:
                this.gameObjectAnger.SetActive(true);
                this.displayedEmotion = this.gameObjectAnger;
                this.displayedImageEmotion = this.displayedEmotion.GetComponent<SpriteRenderer>();
                break;

            case Emotion.sadness:
                this.gameObjectSadness.SetActive(true);
                this.displayedEmotion = this.gameObjectSadness;
                this.displayedImageEmotion = this.displayedEmotion.GetComponent<SpriteRenderer>();
                break;

            case Emotion.disgust:
                this.gameObjectDisgust.SetActive(true);
                this.displayedEmotion = this.gameObjectDisgust;
                this.displayedImageEmotion = this.displayedEmotion.GetComponent<SpriteRenderer>();
                break;

            case Emotion.joy:
                this.gameObjectJoy.SetActive(true);
                this.displayedEmotion = this.gameObjectJoy;
                this.displayedImageEmotion = this.displayedEmotion.GetComponent<SpriteRenderer>();
                break;

            case Emotion.embarrassment:
                this.gameObjectEmbarrassment.SetActive(true);
                this.displayedEmotion = this.gameObjectEmbarrassment;
                this.displayedImageEmotion = this.displayedEmotion.GetComponent<SpriteRenderer>();
                break;
        }
    }
}
