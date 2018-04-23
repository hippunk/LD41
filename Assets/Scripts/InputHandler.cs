using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {

    public InputField input;

    private void Start() {
        this.input.placeholder.GetComponent<Text>().text = GameConstant.PLAYER_NAME;
    }

    public void OnPseudoEdit() {
        GameLogicManager.playerName = this.input.text;
        Debug.Log(GameLogicManager.playerName);
    }

}
