using UnityEngine;
using System.Collections;

public class SpinSphere : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 sideways = new Vector3 (50f, 50f, 0f);
		transform.Rotate(sideways * Time.deltaTime);
	}
}
