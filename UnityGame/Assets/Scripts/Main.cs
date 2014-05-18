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
		int test = ((currentSign + 1) % 3);
		Debug.Log("test? "+test);
		// is win?
		if( in_newSign == test || currentSign == 0 )
		{
			Debug.Log ("WINNER!");

			if(in_newSign == currentSign)
			{
				Debug.Log ("well actually a tie!");
			}

			currentSign = in_newSign;
		}
		else
		{
			Debug.Log ("LOSE!");
		}
	}

}
