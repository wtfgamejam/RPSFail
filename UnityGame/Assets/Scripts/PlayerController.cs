using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int score;
	public int id;
	public Sign currentSign;

	private int m_meter;

	public Sign sign;

	// Use this for initialization
	void Start () {
		//InvokeRepeating ("UpdateMeter", 1.5f, 0.05f);
		renderer.material.SetFloat("_Cutoff", Mathf.InverseLerp(100, 0, meter)); 
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

	void UpdateMeter () {
		if (meter <= 100) {
			meter += 1;
			if ( meter > 0 ){
				renderer.material.SetFloat("_Cutoff", Mathf.InverseLerp(100, 0, meter)); 
			}
		}
	}

	public void StartMeter () {
		InvokeRepeating ("UpdateMeter", 0.1f, 0.05f);
	}

	public void StopMeter () {
		CancelInvoke ();
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

	public int meter
	{
		get {return m_meter;}
		set {m_meter = value;}
	}
}
