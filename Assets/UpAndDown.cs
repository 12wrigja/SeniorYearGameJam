using UnityEngine;
using System.Collections;

public class UpAndDown : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 5, 10) - 3, transform.position.z);
	}
}
