using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

	public float speed;
	private bool inAir = false;
	private float distToGround;
	private GameObject lastPlatform;

	private Rigidbody rb;

	void Start () {
		lastPlatform = GameObject.Find ("platforms (1)");
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		float jump = 0;

		if(Input.GetKeyDown(KeyCode.Space) && !inAir) {
			Debug.Log ("Jumo");
			jump = 200f;
		}

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		Vector3 jumpVect = new Vector3(0.0f, jump, 0.0f);

		rb.AddForce (movement * speed);
		rb.AddForce (jumpVect);
	}

	void OnCollisionExit(Collision other) {
		if (other.gameObject.tag == "Platform") {
			inAir = true;
			transform.parent = null;
		}
	}

	void OnCollisionEnter(Collision other){
		//Debug.Log (other.gameObject.tag);

		if (other.gameObject.tag == "Platform") {
			if (other.gameObject.transform.parent.name == "Platform5") {
				GameObject.FindObjectOfType<UIController> ().EndGame ();
			}
			inAir = false;
			lastPlatform = other.gameObject;
		} else if (other.gameObject.tag == "Bottom") {
			GameObject.Find ("Health").gameObject.GetComponent<Image> ().fillAmount -= .25f;
			transform.rotation = Quaternion.identity;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			transform.position = new Vector3 (lastPlatform.transform.position.x, lastPlatform.transform.position.y + 2, lastPlatform.transform.position.z);
		} else if (other.gameObject.tag == "BlackHole") {
			Debug.Log ("InBlackhole actions");
		}

	}

	void OnCollisionStay(Collision other) {
		if (other.gameObject.tag == "Platform") {
			Debug.Log(other.gameObject.transform.parent.name);
			transform.parent = other.gameObject.transform.parent;
		}
	}



	void OnTriggerStay(Collider other) {
		//Debug.Log (other.gameObject.tag);
		if (other.gameObject.tag == "BlackHole") {
			Vector3 forceDirection = other.transform.position - transform.position;
			// apply force on target towards me
			rb.AddForce (forceDirection.normalized * 400 * Time.fixedDeltaTime);
		} else if (other.gameObject.tag == "BlackHoleInner") {
			Vector3 forceDirection = other.transform.position - transform.position;
			// apply force on target towards me
			rb.AddForce (forceDirection.normalized * 80000 * Time.fixedDeltaTime);
		} 
	}


}