using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimationController : MonoBehaviour {

    [SerializeField] private GameObject table;
    [SerializeField] private GameObject win;

    // Use this for initialization
    void Start() {
        if (GameLogicManager.endResult == 0) {
            this.table.SetActive(true);
            Instantiate<GameObject>(GameLogicManager.currentCharacter.endPrefab);
            return;
        }
        else {
            this.win.SetActive(true);
        }
    }

}
