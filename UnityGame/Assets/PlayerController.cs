using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int score;
	public int meter;
	public int id;
	public int currentSign;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Rock" + id)) {
			Debug.Log("Pressed Rock");
		}

		if (Input.GetButtonDown ("Paper" + id)) {
			Debug.Log("Pressed Paper");
		}

		if (Input.GetButtonDown ("Scissors" + id)) {
			Debug.Log("Pressed Scissors");
		}
	}

}
