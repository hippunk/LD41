using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSound : MonoBehaviour {
	public AudioClip sound;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onClickEvent(){
		PlayMusic.playOnce (sound);
	}
}
