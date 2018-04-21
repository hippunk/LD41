using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour {

    public const float ANGER_VALUE = 0.6f;
    public const float SADNESS_VALUE = 0.7f;
    public const float JOY_VALUE = 1.2f;
    public const float DISGUST_VALUE = 0.5f;
    public const float EMBARRASSMENT_VALUE = 0.9f;
    public const string DIALOG_FILE_PATH = "Dialog";

    [SerializeField]
    private List<CharacterData> characters;

    public Character[] LoadCharacters() {
        Character[] characters = new Character[this.characters.Count];
        int index = 0;
        foreach (var c in this.characters) {
            characters[index] = new Character {
                characterName = c.characterName,
                titre = c.titre,
                relation = c.relation,
                emotion = this.EmotionToFloat(c.emotion),
                dialog = DialogueManager.LoadDialogueFile(DIALOG_FILE_PATH).GetDialogue(c.characterName)
            };
        }
        return characters;
    }

    private float EmotionToFloat(Emotion emotion) {
        switch (emotion) {
            case Emotion.anger:
                return ANGER_VALUE;
            case Emotion.disgust:
                return DISGUST_VALUE;
            case Emotion.embarrassment:
                return EMBARRASSMENT_VALUE;
            case Emotion.joy:
                return JOY_VALUE;
            case Emotion.sadness:
                return SADNESS_VALUE;
            default:
                return 1;
                    
        }
    }
}
