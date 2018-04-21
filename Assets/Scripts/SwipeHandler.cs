using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwipeHandler : MonoBehaviour {

    private List<CharacterData> characterPool = new List<CharacterData>(); //TODO Change with char class later
    public Image currentCharacterProfile;

    private int currentCharacterId = -1;
    //Pool personnage 

    private void Start()
    {
        characterPool = GameObject.Find("DataHandler").GetComponent<CharacterHandler>().GetCharList();

        if (characterPool.Count <= 0)
            Console.Error.WriteLine("Pas de personnages dans le pool");
        else
            Swipe();
    }

    public void OnYes()
    {
        Debug.Log("Yes");
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
        currentCharacterId = (currentCharacterId + 1) % characterPool.Count;
        currentCharacterProfile.sprite = characterPool[currentCharacterId].dateCard;
    }
}
