using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogContent : MonoBehaviour {
	public Text dialogText;

	public void setContent(string text){
		dialogText.text = text;
	}
	public void clear(){
		dialogText.text = "";
	}
}
