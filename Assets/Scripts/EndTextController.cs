using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTextController : MonoBehaviour {

    [SerializeField] private Text text;
    [SerializeField] private string winText;
    [SerializeField] private string looseText;

    // Use this for initialization
    void Start () {
        if (GameLogicManager.endResult == 0) {
            this.text.fontSize = 80;
            this.text.text = this.looseText;
        }
        else {
            this.text.fontSize = 60;
            this.text.text = this.winText;
        }
    }

}
