using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erase : MonoBehaviour {
	public void eraseClicked(){
		if (GameManager.cellClicked != null && GameManager.cellClicked.playable != false) {
			GameManager.cellClicked.txtvalue.text = "";
			GameManager.cellClicked.txtvalue.color = Color.white;
			GameManager.squares.Add (GameManager.cellCoords [0]);
			GameManager.rows.Add (GameManager.cellCoords [1]);
			GameManager.columns.Add (GameManager.cellCoords [2]);
			GameManager.cellValues.Add (GameManager.cellClicked.txtvalue.text);
			GameManager.eraseCalled = true;
		}
	}
}
