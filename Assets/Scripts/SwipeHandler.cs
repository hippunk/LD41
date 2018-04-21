using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeHandler : MonoBehaviour {

    public Sprite[] characterPool; //TODO Change with char class later
    public Image currentCharacterProfile;

    private int currentCharacterId = -1;
    //Pool personnage 

    private void Start()
    {
        if (characterPool.Length <= 0)
            Console.Error.WriteLine("Pas de personnages dans le pool");
        else
            Swipe();
    }

    public void OnYes()
    {
        Debug.Log("Yes");
        Debug.Log("LoadScene Date with char data");
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
        currentCharacterProfile.sprite = characterPool[currentCharacterId];
    }
}
