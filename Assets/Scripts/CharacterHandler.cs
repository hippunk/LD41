using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour {

    [SerializeField]
    private List<CharacterData> characters;

    public List<CharacterData> GetCharList()
    {
        return characters;
    }

    public Character[] LoadCharacters() {
        Character[] characters = new Character[this.characters.Count];
        int index = 0;
        foreach (var c in this.characters) {
            characters[index] = new Character {
                characterName = c.characterName,
                titre = c.titre,
                relation = c.relation,
                age = c.age,
                description = c.description,
                emotion = c.emotion,
                endings = c.endings,
                dialog = DialogueManager.LoadDialogueFile(GameConstant.DIALOG_FILE_PATH).GetDialogue(c.characterName),
                dateCard = c.dateCard,
                charaSprite = c.charaSprite,
                prefab = c.prefab

            };
            index++;
        }
        return characters;
    }
}
