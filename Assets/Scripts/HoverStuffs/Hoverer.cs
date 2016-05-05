using UnityEngine;
using System.Collections;

public class Hoverer : MonoBehaviour {
    public Transform[] hoverPoints;
    public GravityManager gravityManager;
    [Tooltip(@"This is the height that the hoverer will attempt to be above the ground.")]
    public float hoverHeight;
    [Tooltip(@"This is the maximum distance from the surface at which hover forces will be applied")]
    public float maxHoverDist = float.PositiveInfinity;
    [Tooltip("This is the spring constant.  it is inverted (you should provide a negative number).\nJUSTIN: this is the number you want to fiddle with.")]
    public float k;
    //public float b;
    [Tooltip("The rigidbody that forces should be applied to")]
    public Rigidbody rgb;
    [Tooltip("For each hover point, the angle between it's forward axis and the current gravity direction is calculated.  If it exceeds this value, hover physics are not calculated for that point.")]
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
