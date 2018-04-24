using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButtonHandler : MonoBehaviour {

    public void OnRestart() {
        GameObject.Find("Menu UI").GetComponent<StartOptions>().Fade(1);
    }

}
