using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public int currentSign = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Resolve(int in_newSign)
	{
		int test = ((currentSign + 1) % 3) + 1;
		Debug.Log("test? "+test);
		// is win?
		if( in_newSign == test || in_newSign == currentSign )
		{
			Debug.Log ("WINNER!");
			currentSign = in_newSign;
		}
		else
		{
			if(currentSign == 0 ) currentSign = in_newSign;
			Debug.Log ("LOSE!");
		}
	}

}
