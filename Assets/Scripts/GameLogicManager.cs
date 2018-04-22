using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : MonoBehaviour {

    public static Character currentCharacter;

    [HideInInspector]
    public static Dialogue currentDialogue;


    public static void SetCurrentCharacter(Character character)
    {
        currentCharacter = character;
        currentDialogue = character.dialog;
    }


    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);
	}
	

    public static Character GetCurrentCharacter()
    {
        return currentCharacter;
    }
}
