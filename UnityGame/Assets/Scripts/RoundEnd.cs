using UnityEngine;
using System.Collections;

public class RoundEnd : MonoBehaviour {

	public SpriteRenderer player1;
	public SpriteRenderer player2;
	public SpriteRenderer player3;

	public bool isEndGame = false;
	
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

		iTween.ScaleFrom(winner.gameObject, iTween.Hash("easeType", "easeOutElastic",
		                                                "speed", 30,
		                                                "scale", new Vector3(10f,10f,10f),
		                                                "oncomplete", "RestartRound",
		                                                "oncompletetarget", gameObject) );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void RestartRound()
	{
		if(isEndGame)
		{
			StartCoroutine(LoadLevel(2.0f,0));
		}
		else
		{
			StartCoroutine(LoadLevel(2.0f,1));
		}
	}

	IEnumerator LoadLevel(float delay, int level)
	{
		yield return new WaitForSeconds(delay);
		Application.LoadLevel(level);
	}

}
