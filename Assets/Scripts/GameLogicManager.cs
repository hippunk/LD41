using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : MonoBehaviour {

    public static CharacterData currentCharacter;

    public static void SetCurrentCharacter(CharacterData character)
    {
        currentCharacter = character;
    }


    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);
	}
	

    public static CharacterData GetCurrentCharacter()
    {
        return currentCharacter;
    }
}
