using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : MonoBehaviour {

    public static Character currentCharacter = null;
    public Character charaDebug;
    public static string playerName = GameConstant.PLAYER_NAME;
    [HideInInspector]
    public static Dialogue currentDialogue;
    private bool set = false;


    public static void SetCurrentCharacter(Character character) {
        currentCharacter = character;
        currentDialogue = character.dialog;
    }


    // Use this for initialization
    void Start() {
        DontDestroyOnLoad(this);
    }

    private void Update() {
        if (currentCharacter != null && !set) {
            charaDebug = currentCharacter;
            set = true;
        }
    }


    public static Character GetCurrentCharacter() {
        return currentCharacter;
    }
}
