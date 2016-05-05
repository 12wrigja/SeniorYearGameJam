using UnityEngine;
using System.Collections;

public class GravityPull : MonoBehaviour {

	/*public float pullRadius = 2000;
	public float pullForce = 10000;

	public void FixedUpdate() {
		foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius)) {
			if (collider.GetComponent<Rigidbody> () != null) {
				Debug.Log (collider.tag);
				Debug.Log ("Black Hole");
				// calculate direction from target to me
				Vector3 forceDirection = transform.position - collider.transform.position;

				// apply force on target towards me
				collider.GetComponent<Rigidbody> ().AddForce (forceDirection.normalized * pullForce * 100 * Time.fixedDeltaTime);
			}
		}
	}*/

}
