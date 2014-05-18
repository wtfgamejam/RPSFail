using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int score;
	public int id;
	public Sign currentSign;
	
	public MeterController meter;
	public Sign sign;

	// Use this for initialization
	void Start () {
		//InvokeRepeating ("UpdateMeter", 1.5f, 0.05f);
		//renderer.material.SetFloat("_Cutoff", Mathf.InverseLerp(100, 0, meter)); 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Rock" + id)) {
//			Debug.Log("Pressed Rock");
			SendSign(Sign.ROCK_ID);
		}

		if (Input.GetButtonDown ("Paper" + id)) {
//			Debug.Log("Pressed Paper");
			SendSign(Sign.PAPER_ID);
		}

		if (Input.GetButtonDown ("Scissors" + id)) {
//			Debug.Log("Pressed Scissors");
			SendSign(Sign.SCISSORS_ID);
		}
		
		if (Input.GetButtonDown ("Whoa" + id)) {
//			Debug.Log("Pressed Whoa");
		}


	}

	public void StartMeter () {
		meter.StartMeter();
	}
	
	public void StopMeter () {
		meter.StopMeter();
	}

	public void SendSign( int signType )
	{
		if (currentSign == null) { 
			Sign signClone = (Sign) Instantiate(sign, transform.position, transform.rotation);
			signClone.transform.parent = gameObject.transform.parent;
			signClone.Initialize(signType, id);
			currentSign = signClone;
		}
	}


}
