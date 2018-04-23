using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {

    public InputField input;

    public void OnPseudoEdit() {
        GameLogicManager.playerName = this.input.text;
        Debug.Log(GameLogicManager.playerName);
    }

}
