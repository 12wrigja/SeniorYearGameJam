using UnityEngine;
using System.Collections;

public class Hoverer : MonoBehaviour {
    public Transform[] hoverPoints;
    public GravityManager gravityManager;
    public float hoverHeight;
    public float maxHoverDist = float.PositiveInfinity;
    public float k;
    //public float b;
    public Rigidbody rgb;
    [Range(0,180)]
    public float cutoffAngle = 90f;


    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        foreach (Transform t in hoverPoints)
            Gizmos.DrawSphere(t.position, 0.1f);
    }

    // Update is called once per frame
    void FixedUpdate () {
	    foreach(var hoverPoint in hoverPoints) {
            Debug.DrawRay(hoverPoint.position, gravityManager.GravityDirection, Color.red);
            if (Vector3.Angle(hoverPoint.forward, gravityManager.GravityDirection) < cutoffAngle) {
                RaycastHit hit;
                if (Physics.Raycast(hoverPoint.position, gravityManager.GravityDirection, out hit, maxHoverDist, ~LayerMask.GetMask("Car"))) {
                    Debug.DrawRay(hit.point, hit.normal, Color.green);
                    float delta = hit.distance - hoverHeight;
                    float force = k * delta;
                    //if (delta < 0f) {
                    //    force *= (hoverHeight / hit.distance);
                    //}
                    Vector3 forceVector = hit.normal * force;
                    rgb.AddForceAtPosition(forceVector, hoverPoint.position, ForceMode.Force);
                    Debug.DrawRay(hoverPoint.position, forceVector, Color.blue);
                }
            }
        }
	}
}
