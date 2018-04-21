using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTest : MonoBehaviour {

    [SerializeField]
    private CharacterHandler characterHandler;

    // Use this for initialization
    void Start() {
        var characters = this.characterHandler.LoadCharacters();
        foreach (var c in characters) {
            Debug.Log(c.dialog.GetChoices()[0]);
            Debug.Log(c.characterName + " " + c.titre + " (" + c.emotion + ") : " + c.dialog.GetChoices()[0].dialogue);
        }
    }

}
