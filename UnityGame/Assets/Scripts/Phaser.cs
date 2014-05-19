using UnityEngine;
using System.Collections;


public class Phaser : MonoBehaviour
{
	
	private LineRenderer lineRenderer;
	private float counter;
	private float dist;

	public Transform origin;
	public Transform destination;

	public float lineDrawSpeed = 6f;	


	// Use this for initialization
	void Start ()
	{
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.SetPosition (0, new Vector3(origin.position.x, 
		                                         origin.position.y, 
		                                         origin.position.z
		                                         ));
		lineRenderer.SetWidth (0.2f, 0.2f); 

		dist = Vector3.Distance (origin.position, destination.position);
	}

	// Update is called once per frame
	void Update ()
	{
		if (counter < dist) {
			counter += .1f / lineDrawSpeed;

			float x = Mathf.Lerp (0, dist, counter);

			Vector3 pointA = new Vector3(origin.position.x, 
			                             origin.position.y, 
			                             origin.position.z
			                             );
			Vector3 pointB = new Vector3(destination.position.x, 
			                             destination.position.y, 
			                             destination.position.z 
			                             );
			
			Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

			lineRenderer.SetPosition(1, pointAlongLine);

		}
	}
}

