using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int score;
	public int meter;
	public int id;
	public int currentSign;

	public Sign sign;

	// Use this for initialization
	void Start () {
		currentSign = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Rock" + id)) {
			Debug.Log("Pressed Rock");
			SendSign(Sign.ROCK_ID);
		}

		if (Input.GetButtonDown ("Paper" + id)) {
			Debug.Log("Pressed Paper");
			SendSign(Sign.PAPER_ID);
		}

		if (Input.GetButtonDown ("Scissors" + id)) {
			Debug.Log("Pressed Scissors");
			SendSign(Sign.SCISSORS_ID);
		}
		
		if (Input.GetButtonDown ("Whoa" + id)) {
			Debug.Log("Pressed Whoa");
		}
	}

	public void SendSign( int signType )
	{
		if (currentSign == 0) { 
			currentSign = signType;
			Sign signClone = (Sign) Instantiate(sign, transform.position, transform.rotation);
			signClone.transform.parent = gameObject.transform.parent;
			signClone.Initialize(signType, id);
		}
	}
}
