using UnityEngine;
using System.Collections;

public class BackAndForth : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(Mathf.PingPong(Time.time * 3, 10) - 5, transform.position.y, transform.position.z);
	}
}
