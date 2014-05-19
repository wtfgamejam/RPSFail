using UnityEngine;
using System.Collections;

public class RoundEnd : MonoBehaviour {

	public SpriteRenderer player1;
	public SpriteRenderer player2;
	public SpriteRenderer player3;
	
	// Use this for initialization
	void Start () {

		player1.enabled = false;
		player2.enabled = false;
		player3.enabled = false;

		SpriteRenderer winner = player1;
		int currentWinner = PlayerPrefs.GetInt("CurrentWinner", 1);

		if(currentWinner == 1)
		{
			winner = player1;
		}
		else if(currentWinner == 2)
		{
			winner = player2;
		}
		else if(currentWinner == 3)
		{
			winner = player3;
		}

		winner.enabled = true;

		iTween.RotateAdd(winner.gameObject, iTween.Hash("y",720,
		                                                "easeType", "easeOutSine",
		                                         		"speed", 300,
		                                                "oncomplete", "RestartRound",
		                                                "oncompletetarget", gameObject) );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void RestartRound()
	{
		StartCoroutine(LoadLevel(2.0f,2));
	}

	IEnumerator LoadLevel(float delay, int level)
	{
		yield return new WaitForSeconds(delay);
		Application.LoadLevel(level);
	}

}
