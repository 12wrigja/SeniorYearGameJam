using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	public GameObject youWinText;
	public GameObject healthBar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame() {
		GameObject.Find ("TapToPlayText").SetActive (false);
		healthBar.SetActive (true);
		Camera.main.GetComponent<MoveCamera> ().enabled = true;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Controller> ().enabled = true;
	}

	public void EndGame() {
		youWinText.SetActive (true);
		GameObject.Find ("Main Camera").GetComponent<MoveCamera> ().enabled = false;
	}
}
