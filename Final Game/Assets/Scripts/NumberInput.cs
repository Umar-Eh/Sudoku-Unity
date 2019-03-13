using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NumberInput : MonoBehaviour {

	public Text txtvalue;
	ColorBlock cellColor;
	public bool playable = true;

	public void cellPressed(){
		GameManager.btnType = 0;
		GameManager.cellClicked = this;
		GameManager.clickedValue = GetComponent<NumberInput>().txtvalue.text;
	}

	public void setColor(){
		cellColor = this.GetComponent<Button> ().colors;
		if (!playable) {
			cellColor.normalColor = Color.black;
			this.GetComponent<Button> ().colors = cellColor;
		}
	}
}
