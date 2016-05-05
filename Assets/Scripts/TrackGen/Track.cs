using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Track : MonoBehaviour
{
	
	public TrackChunk trackStartPrefab;

	public TrackChunk[] trackSegmentPrefabs;

	public TrackChunk trackFinishPrefab;

	public int trackLength = 1;

	// Use this for initialization
	void Start ()
	{
		TrackChunk startChunk = Instantiate (trackStartPrefab) as TrackChunk;
		if (startChunk == null) {
			Debug.LogError ("The track start chunk does not have a track chunk component.");
			return;
		}

		if (startChunk.GetOpenGates ().Count != 1) {
			Debug.LogError ("The track start chunk does not have exactly one open gate.");
		}

		TrackChunk endChunk = Instantiate (trackFinishPrefab) as TrackChunk;
		if (endChunk == null) {
			Debug.LogError ("The track end chunk does not have a track chunk component.");
			return;
		}

		if (endChunk.GetOpenGates ().Count != 1) {
			Debug.LogError ("The track end chunk does not have exactly one open gate.");
		}

		for (int i = 0; i < trackSegmentPrefabs.Length; i++) {
			TrackChunk obj = trackSegmentPrefabs [i];
			if (obj.GetOpenGates ().Count != 2) {
				Debug.LogError ("The track chunk in index " + i + " does not have exactly two open gate.");
				return;
			}
		}

		startChunk.transform.position = this.transform.position;
		startChunk.transform.rotation = this.transform.rotation;
		startChunk.transform.SetParent (this.transform);

		TrackChunk currentChunk = startChunk;
		Direction previousDirection = startChunk.GetOpenGates () [0].GateDirection;
		for (int i = 0; i < trackLength; i++) {
			//Generate a new piece that matches up with the other pieces.
			Direction opposingDirection = previousDirection.GetOpposite ();
			List<TrackChunk> availableTrackParts = new List<TrackChunk> ();
			foreach (TrackChunk obj in trackSegmentPrefabs) {
				if (obj.GetGateForDirection (opposingDirection) != GateSize.CLOSED) {
					availableTrackParts.Add (obj);
					break;
				}
			}
			TrackChunk selectedTrackPart = Instantiate (availableTrackParts [Random.Range (0, availableTrackParts.Count - 1)]) as TrackChunk;
			TrackChunk nextChunk = selectedTrackPart.GetComponent<TrackChunk> ();
			selectedTrackPart.transform.SetParent (this.transform);
			selectedTrackPart.name = "Track Segment " + i;

			//Place chunk in world space
			selectedTrackPart.PositionSideAtPoint (currentChunk.GetGatePosition (previousDirection), opposingDirection);

			currentChunk = nextChunk;
			Direction exitDirection = currentChunk.getExitDirection (opposingDirection);
			previousDirection = exitDirection;
		}
		endChunk.PositionSideAtPoint (currentChunk.GetGatePosition (previousDirection), endChunk.GetOpenGates () [0].GateDirection);
		endChunk.transform.SetParent (this.transform);
	}
}
