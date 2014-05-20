using UnityEngine;
using System.Collections;

public class TitleScript : MonoBehaviour {

	public SpriteRenderer rock;
	public SpriteRenderer paper;
	public SpriteRenderer scissors;

	public SpriteRenderer controls;

	private bool player1Ready = false;
	private bool player2Ready = false;
	private bool player3Ready = false;

	private bool isReady = false;
	private bool showControls = false; 

	// Use this for initialization
	void Start () {
		controls.enabled = false;

		PlayerPrefs.DeleteAll();

		iTween.MoveTo(GameObject.Find("rock"), iTween.Hash("y",1,
		                                                   "delay", 0f,
		                                                   "easeType", "easeOutElastic",
		                                                   "islocal",true));

		iTween.MoveTo(GameObject.Find("paper"), iTween.Hash("y",1,
		                                                    "delay", 1f,
		                                                    "easeType", "easeOutElastic",
		                                                    "islocal",true));

		iTween.MoveTo(GameObject.Find("scissors"), iTween.Hash("y",1,
		                                                       "delay", 2f,
		                                                       "easeType", "easeOutElastic",
		                                                       "islocal",true));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Select1") ||
		    Input.GetButtonDown ("Select1") ||
		    Input.GetButtonDown ("Select1") ||
		    Input.GetKeyDown(KeyCode.Escape))
		{
			showControls = !showControls;
			if(showControls){
				GameObject.Find("Player1Text").guiText.enabled = false;
				GameObject.Find("Player2Text").guiText.enabled = false;
				GameObject.Find("Player3Text").guiText.enabled = false;
				GameObject.Find("PressStart1").guiText.enabled = false;
				GameObject.Find("PressStart2").guiText.enabled = false;
				GameObject.Find("PressStart3").guiText.enabled = false;
				controls.enabled = true;
			}
			else
			{
				GameObject.Find("Player1Text").guiText.enabled = true;
				GameObject.Find("Player2Text").guiText.enabled = true;
				GameObject.Find("Player3Text").guiText.enabled = true;
				GameObject.Find("PressStart1").guiText.enabled = true;
				GameObject.Find("PressStart2").guiText.enabled = true;
				GameObject.Find("PressStart3").guiText.enabled = true;

				controls.enabled = false;
			}
		}

		if (Input.GetButtonDown ("Start1")) {
			Debug.Log("Pressed Start 1");
			rock.color = new Color(.963F, 0.428F, 0.428F, 1F);
			player1Ready = true;
		}
		if (Input.GetButtonDown ("Start2")) {
			Debug.Log("Pressed Start 2");
			paper.color = new Color(0.3F, 0.950F, 0.711F, 1F);
			player2Ready = true;
		}
		if (Input.GetButtonDown ("Start3")) {
			Debug.Log("Pressed Start 3");
			scissors.color = new Color(0.591F, 0.861F, 0.961F, 1F);
			player3Ready = true;
		}

		if(player1Ready && player2Ready && player3Ready && !isReady)
		{
			isReady = true;
			Ready();
		}
	}


	public void Ready()
	{
		Debug.Log("GOGOGOGOGOGOGO");
		Application.LoadLevel (1);
	}
}
