using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

	static public FollowCam S;
	public float easing = 0.5f;
	public bool _______________;
	public GameObject poi;
	public float camz;


	void Awake(){
		S = this;
		camz = this.transform.position.z;
	}
		
	
	// Update is called once per frame
	void Update () {
		if (poi == null)
			return;

		Vector3 destination = poi.transform.position;
		destination = Vector3.Lerp (transform.position, destination, easing);
		destination.z = camz;
		transform.position = destination;
		
	}
}
