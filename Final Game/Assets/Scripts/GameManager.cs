using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static NumberInput[,,] grid = new NumberInput[9,3,3];
	public static bool init= false;
	public static string clickedValue;
	public static int btnType;
	public static int chosenValue;
	public GetChildren childScript;
	public static NumberInput cellClicked;
	public static int[] cellCoords = new int[3];
	public static bool eraseCalled = false;
	private int count;
	public static List<int> squares = new List<int>();
	public static List<int> rows = new List<int>();
	public static List<int> columns = new List<int>();
	public static List<string> cellValues = new List<string>();
	public GameObject validatePanel;
	public GameObject restartPanel;
	public GameObject donePanel;
	public GameObject wrongSolutionPanel;
	public GameObject emptySolutionPanel;
	public GameObject correctSolutionPanel;

	static int[,,] sudoMatrix = { { { 0, 5, 2}, { 0, 0, 6}, { 1, 0, 8} }, { { 0, 7, 1 }, { 0, 4, 0 }, { 0, 0, 2 } }, { { 0, 4, 8}, { 1, 2, 5}, { 7, 0, 9} }, { { 2, 0, 5}, { 8, 0, 4}, { 6, 0, 7} }
		,{ { 4, 9, 0}, { 0, 1, 6}, { 5, 8, 3} }, { { 8, 1, 6}, { 5, 3, 7}, { 2, 0, 4} }, { { 0, 2, 0}, { 7, 6, 1}, { 4, 0, 9} }, { { 7, 6, 4}, { 8, 0, 9}, { 0, 2, 5} }, { { 0, 8, 1}, { 4, 5, 2}, { 0, 7, 3} } };

	void Awake(){
		btnType = 9;
	}


	// Update is called once per frame
	void Update () {
		if (!init)
			initializeLevel ();

		if (eraseCalled) {
			childScript.gridcellClicked();
			eraseCalled = false;
		}

		if (Input.GetMouseButtonUp (0))
			buttonPressed ();

	}

	void initializeLevel(){
		validatePanel.SetActive (false);
		restartPanel.SetActive (false);
		donePanel.SetActive (false);
		wrongSolutionPanel.SetActive (false);
		emptySolutionPanel.SetActive (false);
		correctSolutionPanel.SetActive (false);
		count = 0;
		for (int square = 0; square < 9; square++) {
			for (int row = 0; row < 3; row++) {
				for (int column = 0; column < 3; column++) {
					grid [square, row, column] = childScript.cells [count];
					count++;
				}
			}
		}
		for (int square = 0; square < 9; square++) {
			for (int row = 0; row < 3; row++) {
				for (int column = 0; column < 3; column++) {
					if (sudoMatrix [square, row, column] != 0) {
						grid [square, row, column].txtvalue.text = "" + sudoMatrix [square, row, column];
						grid [square, row, column].playable = false;
					} else {
						grid [square, row, column].txtvalue.text = "";
						grid [square, row, column].playable = true;
					}
				}
			}
		}
		for (int i = 0; i < 81; i++) {
			childScript.cells[i].setColor();
			if (childScript.cells[i].playable)
				childScript.cells[i].txtvalue.text = "";
		}

		if (!init)
			init = true;
	}

	public void buttonPressed(){

		if (btnType == 0) {
			cellCoords [0] = childScript.cells.FindIndex (x => x == cellClicked) / 9;
			cellCoords [1] = (childScript.cells.FindIndex (x => x == cellClicked) / 3) - (3 * cellCoords [0]);
			cellCoords [2] = (childScript.cells.FindIndex (x => x == cellClicked) - (9 * cellCoords [0])) % 3;
			childScript.gridcellClicked ();
			btnType = 9;
		}
	}

	public void Undo(){
		if (squares.Count > 1) {
			grid [squares [squares.Count - 2], rows [rows.Count - 2], columns [columns.Count - 2]].txtvalue.text = cellValues [cellValues.Count - 2];
			squares.RemoveAt (squares.Count - 1);
			rows.RemoveAt (rows.Count - 1);
			columns.RemoveAt (columns.Count - 1);
			cellValues.RemoveAt (cellValues.Count - 1);
		} 
	}

	public void DonePressed(){
		donePanel.SetActive (true);
	}

	public void DoneConfirmed(){
		donePanel.SetActive (false);
		for (int square = 0; square < 9; square++) {
			for (int row = 0; row < 3; row++) {
				for (int column = 0; column < 3; column++) {
					if (grid [square, row, column].txtvalue.text != "") {
						if (!Program.Checker (square, row, column)) {
							wrongSolutionPanel.SetActive (true);
							return;
						}
					} else {
						emptySolutionPanel.SetActive (true);
						return;
					}
				}
			}
		}
		correctSolutionPanel.SetActive (true);
	}

	public void correctPanelQuit(){
		correctSolutionPanel.SetActive (false);
		SceneManager.LoadScene ("Main");
	}

	public void wrongPanelClicked(){
		wrongSolutionPanel.SetActive (false);
	}

	public void emptyPanelClicked(){
		emptySolutionPanel.SetActive (false);
	}

	public void CancelDone(){
		donePanel.SetActive (false);
	}

	public void Validate(){
		validatePanel.SetActive (true);
	}

	public void ValidateClicked(){
		validatePanel.SetActive (false);
		if (cellClicked != null && cellClicked.txtvalue.text != "") {
			if (Program.Checker (cellCoords [0], cellCoords [1], cellCoords [2]))
				print ("true");
			else {
				print ("false");
				cellClicked.txtvalue.color = Color.red;
			}
		} else
			print ("null selected");
	}

	public void cancelValidate(){
		validatePanel.SetActive (false);
	}

	public void restartClicked(){
		restartPanel.SetActive (true);
	}

	public void restartYes(){
		restartPanel.SetActive (false);
		correctSolutionPanel.SetActive (false);
		init = false;
		clickedValue = "";
		btnType = 9;
		chosenValue = 0;
		cellClicked = null;
		squares.Clear ();
		rows.Clear ();
		columns.Clear ();
		cellValues.Clear ();
	}

	public void cancelRestat(){
		restartPanel.SetActive (false);
	}


	public void BackGroundClicked(){
		cellClicked = null;
		clickedValue = "";
		chosenValue = 0;
	}

	public void chooseNum(int i){
		btnType = 1;
		switch(i){
		default:
			break;
		case(0):
			chosenValue = 1;
			break;
		case(1):
			chosenValue = 2;
			break;
		case(2):
			chosenValue = 3;
			break;
		case(3):
			chosenValue = 4;
			break;
		case(4):
			chosenValue = 5;
			break;
		case(5):
			chosenValue = 6;
			break;
		case(6):
			chosenValue = 7;
			break;
		case(7):
			chosenValue = 8;
			break;
		case(8):
			chosenValue = 9;
			break;
		}
		clickedValue = ""+chosenValue;
		childScript.gridcellClicked ();
		if (cellClicked != null) {
			if (chosenValue != 0 && cellClicked.playable) {
				cellClicked.txtvalue.color = Color.white;
				if (cellClicked.txtvalue.text == "") {
					squares.Add(cellCoords[0]);
					rows.Add(cellCoords[1]);
					columns.Add(cellCoords[2]);
					cellValues.Add ("");
				}
				cellClicked.txtvalue.text = "" + chosenValue;
				childScript.gridcellClicked ();
				squares.Add(cellCoords[0]);
				rows.Add(cellCoords[1]);
				columns.Add(cellCoords[2]);
				cellValues.Add (cellClicked.txtvalue.text);
				chosenValue = 0;
				clickedValue = "";
			}
		}
	}
	
}
