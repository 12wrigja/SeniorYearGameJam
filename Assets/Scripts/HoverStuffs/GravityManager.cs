using UnityEngine;
using System.Collections;

public class GravityManager : MonoBehaviour {

    public float GravityPollDistance = 1f;

    [System.NonSerialized]
    public Vector3 GravityDirection = Vector3.down;
	
	// Update is called once per frame
	void FixedUpdate () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, GravityPollDistance, LayerMask.GetMask("Track"))) {
            GravityDirection = -hit.normal;
        }
	}
}
