using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {
	public GameObject prefabProjectile;
	public float velocityMulti = 4f;
	public bool ________;

	public GameObject launchPoint;
	public Vector3 launchPos;
	public GameObject projectile;
	public bool AimingMode;

	Rigidbody ProjectileShoot;

	void Start(){
		ProjectileShoot = projectile.GetComponent<Rigidbody>();
	}

	void Update(){
		if (!AimingMode) {
			return;
		}
			
		Vector3 mousePos2D = Input.mousePosition;
		mousePos2D.z = -Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint (mousePos2D);
		Vector3 mouseDelta = mousePos3D - launchPos;
		float maxMagnitude = this.GetComponent<SphereCollider> ().radius;
		if (mouseDelta.magnitude > maxMagnitude) {
			mouseDelta.Normalize ();
			mouseDelta *= maxMagnitude;
		}
		Vector3 projPos = launchPos + mouseDelta;
		projectile.transform.position = projPos;

		if (Input.GetMouseButtonUp (0)) {
			AimingMode = false;
			ProjectileShoot.isKinematic = false;
			ProjectileShoot.velocity = -mouseDelta * velocityMulti;
			FollowCam.S.poi = projectile;
			projectile = null;
		}
	}

	void Awake(){
		Transform launchPointTrans = transform.Find ("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive (false);

		launchPos = launchPointTrans.position;

	}

	void OnMouseEnter(){
		//print ("Slingshot: OnMouseEnter()");
		launchPoint.SetActive (true);
	}

	void OnMouseExit(){
		//print ("Slingshot: OnMouseExit()");
		launchPoint.SetActive (false);
	}


	void OnMouseDown(){
		AimingMode = true;
		projectile = Instantiate (prefabProjectile) as GameObject;
		projectile.transform.position = launchPos;

		ProjectileShoot.isKinematic = true;
	}


}
