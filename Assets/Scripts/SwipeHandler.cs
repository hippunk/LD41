using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwipeHandler : MonoBehaviour {

    private Character[] characterPool; //TODO Change with char class later
    public Image currentCharacterProfile;

    private int currentCharacterId = -1;
    //Pool personnage 

    private void Start()
    {
        CharacterHandler charHandler = GameObject.Find("DataHandler").GetComponent<CharacterHandler>();
        characterPool  = charHandler.LoadCharacters();

        if (characterPool.Length <= 0)
            Console.Error.WriteLine("Pas de personnages dans le pool");
        else
            Swipe();
    }

    public void OnYes()
    {
        //Load chara in GameManager

        Character curentCharacter = characterPool[currentCharacterId];
        GameLogicManager.SetCurrentCharacter(curentCharacter);
        SceneManager.LoadScene(2);
    }

    public void OnNo()
    {
        Debug.Log("No");
        Swipe();
        Debug.Log("New char ID : ");
    }

    public void Swipe()
    {
        currentCharacterId = (currentCharacterId + 1) % characterPool.Length;
        currentCharacterProfile.sprite = characterPool[currentCharacterId].dateCard;
    }
}
