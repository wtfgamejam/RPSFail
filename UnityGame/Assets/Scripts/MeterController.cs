using UnityEngine;
using System.Collections;

public class MeterController : MonoBehaviour {

	private int m_meter;

	// Use this for initialization
	void Start () {
		renderer.material.SetFloat("_Cutoff", Mathf.InverseLerp(100, 0, meter));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void IncreaseMeter () {
		if (meter <= 100) {
			meter += 2;
			if ( meter > 0 ){
				renderer.material.SetFloat("_Cutoff", Mathf.InverseLerp(100, 0, meter)); 
			}
		}
	}

	public void DecreaseMeter () {
		if (meter > 0) {
			meter -= 1;
			renderer.material.SetFloat("_Cutoff", Mathf.InverseLerp(100, 0, meter)); 
		}
	}

	public void StartMeter () {
		InvokeRepeating ("IncreaseMeter", 0.1f, 0.075f);
	}
	
	public void StopMeter () {
		CancelInvoke ();
	}


	public int meter
	{
		get {return m_meter;}
		set {m_meter = value;}
	}
}
