using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstant : MonoBehaviour {

    public const float ANGER_VALUE = 0.6f;
    public const float SADNESS_VALUE = 0.7f;
    public const float JOY_VALUE = 1.2f;
    public const float DISGUST_VALUE = 0.5f;
    public const float EMBARRASSMENT_VALUE = 0.9f;
    public const string DIALOG_FILE_PATH = "Dialog";
    public const string PLAYER_NAME_TEMPLATE = "[PP]";
    public const string PLAYER_NAME = "Drargka";
    public const float MUSIC_FADE_TIME = 1;
    public const int END_SCENE_NUMBER = 3;

    public static Emotion StringToEmotion(string emotion) {
        if (emotion == Emotion.joy.ToString()) {
            return Emotion.joy;
        }
        if (emotion == Emotion.sadness.ToString()) {
            return Emotion.sadness;
        }
        if (emotion == Emotion.anger.ToString()) {
            return Emotion.anger;
        }
        if (emotion == Emotion.disgust.ToString()) {
            return Emotion.disgust;
        }
        if (emotion == Emotion.embarrassment.ToString()) {
            return Emotion.embarrassment;
        }
        if (emotion == Emotion.neutral.ToString()) {
            return Emotion.neutral;
        }
        return Emotion.none;
    }

    public static float EmotionToFloat(Emotion emotion) {
        switch (emotion) {
            case Emotion.anger:
                return GameConstant.ANGER_VALUE;
            case Emotion.disgust:
                return GameConstant.DISGUST_VALUE;
            case Emotion.embarrassment:
                return GameConstant.EMBARRASSMENT_VALUE;
            case Emotion.joy:
                return GameConstant.JOY_VALUE;
            case Emotion.sadness:
                return GameConstant.SADNESS_VALUE;
            default:
                return 1;

        }
    }
    public static float EmotionToFloat(string emotion) {
        if (emotion == Emotion.joy.ToString()) {
            return GameConstant.JOY_VALUE;
        }
        if (emotion == Emotion.sadness.ToString()) {
            return GameConstant.SADNESS_VALUE;
        }
        if (emotion == Emotion.anger.ToString()) {
            return GameConstant.ANGER_VALUE;
        }
        if (emotion == Emotion.disgust.ToString()) {
            return GameConstant.DISGUST_VALUE;
        }
        if (emotion == Emotion.embarrassment.ToString()) {
            return GameConstant.EMBARRASSMENT_VALUE;
        }
        return 1;
    }

}

public enum Emotion { joy, sadness, anger, disgust, embarrassment, neutral, none }
