using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Instantiate<GameObject>(GameLogicManager.currentCharacter.prefab);
	}

}
