using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour {

    [SerializeField] private GameObject table;
    [SerializeField] private GameObject win;

    // Use this for initialization
    void Start() {
        if (GameLogicManager.endResult == 1) {
            this.win.SetActive(true);
        }
        else {
            this.table.SetActive(true);
            Instantiate<GameObject>(GameLogicManager.currentCharacter.endPrefab);
        }
    }

    public void OnRestart() {
        this.win.SetActive(false);
        this.table.SetActive(false);
        GameObject.Find("Menu UI").GetComponent<StartOptions>().Fade(1);
    }

}
