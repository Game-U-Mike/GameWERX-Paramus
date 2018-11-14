using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {



	/*Last time we created two new boolean variables to be able to jump once and only once in the wind.
	 * We have inwind which lets us know we're in the wind, gets set in ontriggerenter/exit. Already done.
	 * We have stable, which is what we'll use to be able to jump when in the wind even though we're not grounded. Can jump if grounded OR if stable is true and inwind is true.
	 * After jumping in wind, set stable to false. Wont be set true again until grounded, so player can't jump in and out of wind to reset it anymore.	 * 
	 * */


	public int Jumping = 10;
	public int speed = 5;
	public int turnspeed;
	Rigidbody rb;
	public bool grounded;
	public bool stable;
	public bool inwind;
	public bool bounced;
	public float airSpeed;
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		if (grounded == true) {
			
			if (Input.GetKey (KeyCode.W))
				rb.velocity = new Vector3 (transform.forward.x * speed, rb.velocity.y, transform.forward.z * speed);

			if (Input.GetKeyUp (KeyCode.W))
				rb.velocity = new Vector3 (0, rb.velocity.y, 0);

			if (Input.GetKey (KeyCode.S))
				rb.velocity = new Vector3 (-transform.forward.x * speed, rb.velocity.y, transform.forward.z * -speed);

			if (Input.GetKeyUp (KeyCode.S))
				rb.velocity = new Vector3 (0, rb.velocity.y, 0);

		/*	if (Input.GetKey (KeyCode.Q))
				rb.velocity = new Vector3 (rb.velocity.x + -transform.right.x * speed, rb.velocity.y,rb.velocity.z + -transform.right.z * speed);
			if (Input.GetKey (KeyCode.E))
				rb.velocity = new Vector3 (rb.velocity.x + transform.right.x * speed, rb.velocity.y,rb.velocity.z + transform.right.z * speed);*/
			if (Input.GetKey (KeyCode.A))
				gameObject.transform.Rotate (0, -turnspeed, 0);
			if (Input.GetKey (KeyCode.D))
				gameObject.transform.Rotate (0, turnspeed, 0);
			
		} else {	
			if (Input.GetKey (KeyCode.W))
				rb.velocity = new Vector3 (transform.forward.x * speed * airSpeed, rb.velocity.y, transform.forward.z * speed * airSpeed);

			if (Input.GetKey (KeyCode.S))
				rb.velocity = new Vector3 (-transform.forward.x * speed * airSpeed, rb.velocity.y, -transform.forward.z * speed * airSpeed);

			/*if (Input.GetKey (KeyCode.Q))
				rb.velocity = new Vector3 (-speed *airSpeed, rb.velocity.y, rb.velocity.z);
			if (Input.GetKey (KeyCode.E))
				rb.velocity = new Vector3 ( speed* airSpeed, rb.velocity.y, rb.velocity.z);*/
			if (Input.GetKey (KeyCode.A))
				gameObject.transform.Rotate (0, -turnspeed, 0);
			if (Input.GetKey (KeyCode.D))
				gameObject.transform.Rotate (0, turnspeed, 0);
		}

		if (Input.GetKeyDown (KeyCode.Space))
			jump ();

	}
		
	

	void jump () {
		if ( grounded == true )
		rb.velocity = new Vector3 (rb.velocity.x, Jumping, rb.velocity.z);
		if (stable == true && inwind == true) {
			rb.velocity = new Vector3 (rb.velocity.x, Jumping * 4
				, rb.velocity.z);
			stable = false;

		}
	}

	void OnTriggerStay (Collider other) {
		if (other.tag == "Ground" || other.tag == "movingplatform"){
			grounded = true;
			stable = true;
			bounced = false;
		}
	}
	void OnTriggerExit (Collider other) {
		if (other.tag == "Ground" || other.tag == "movingplatform")
			grounded = false;
		if (other.tag == "movingplatform")
		gameObject.transform.parent = null;

		if (other.tag == "windgeyser")
			inwind = false;
	}
	void OnTriggerEnter (Collider other) {
		if (other.tag == "movingplatform")
			gameObject.transform.parent = other.transform;
		if (other.tag == "windgeyser")
			inwind = true;
	}
	void OnCollisionEnter (Collision other) {
		if (other.gameObject.tag == "bounce"){
			rb.velocity = new Vector3 (rb.velocity.x, Jumping * 2, rb.velocity.z);
			bounced = true;
			}
		}
}
