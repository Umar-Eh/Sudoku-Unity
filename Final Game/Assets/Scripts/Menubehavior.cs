using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menubehavior : MonoBehaviour {
	public void triggerMenubehavior(int i){
		switch(i){
			default:
			case(0):
				SceneManager.LoadScene("Level");
				break;
			case(1):
				Application.Quit();
				break;
		}
	}
}
