using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GetChildren : MonoBehaviour {
	public Transform[] obj;
	public List<NumberInput> cells;
	static ColorBlock cellColor;
	// Use this for initialization
	void Start () {
		obj = GetComponentsInChildren<Transform>(true);	
		foreach (Transform ob in obj)
			if (ob.tag == "cell" )
				cells.Add(ob.gameObject.GetComponent<NumberInput>());
	}
	
	public void gridcellClicked(){
		foreach (NumberInput cell in cells) {
			if (cell.txtvalue.text == GameManager.clickedValue && GameManager.clickedValue != "") {
				cellColor = cell.GetComponent<Button> ().colors;
				cellColor.normalColor = cellColor.highlightedColor;
				cell.GetComponent<Button> ().colors = cellColor;
			} else {
				cellColor = cell.GetComponent<Button> ().colors;
				if (cell.GetComponent<NumberInput> ().playable)
					cellColor.normalColor = new Color32 (96, 111, 107, 255);
				else 
					cellColor.normalColor = Color.black;
				cell.GetComponent<Button> ().colors = cellColor;

			}
		}
	}

	public void placeValue(){
		
	}
}
