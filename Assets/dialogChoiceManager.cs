using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogChoiceManager : MonoBehaviour {
	public Dialogue.Choice[] choiceBundle;
	public List<Button> choiceList;
	public Object choicePrefab;

	// Use this for initialization
	void Start () {
		choiceList = new List<Button> ();
		choicePrefab = Resources.Load ("Prefabs/ChoiceButton");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setupChoices(Dialogue choices){
		GameObject tmp;
		ChoiceButton tmpButton;

		choiceBundle = choices.GetChoices();

		if (choiceBundle.Length > 0) {
			foreach (Dialogue.Choice elem in choiceBundle) {
				tmp = Instantiate (choicePrefab) as GameObject;
                tmp.transform.SetParent(this.gameObject.transform);
				tmpButton = tmp.GetComponent<ChoiceButton> ();
				tmpButton.choiceElem = elem;
				tmpButton.choiceText.text = tmpButton.choiceElem.dialogue;

			}
		}
	}

}
