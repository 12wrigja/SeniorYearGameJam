using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Track : MonoBehaviour
{
	public TrackChunk[] trackSegmentPrefabs;

	public int trackLength = 1;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < trackSegmentPrefabs.Length; i++) {
			TrackChunk obj = trackSegmentPrefabs [i];
			if (obj != null) {
				if (!obj.canBeStart && !obj.canBeEnd && obj.GetOpenGates ().Count != 2) {
					Debug.LogError ("The track chunk in index " + i + " does not have exactly two open gates.");
					return;
				} else if ((obj.canBeStart || obj.canBeEnd) && obj.GetOpenGates ().Count != 1) {
					Debug.LogError ("The track chunk in index " + i + " has more or less than 1 open gate. Start or end tiles need to have exactly one open gate.");
				}
			}
		}


		List<TrackChunk> potentialStartTiles = trackSegmentPrefabs.Where ((chunk) => chunk != null && chunk.canBeStart).ToList ();
		if (potentialStartTiles.Count == 0) {
			Debug.LogError ("Unable to begin track - no potential start tiles.");
			return;
		}
		TrackChunk startChunk = Instantiate(potentialStartTiles [Random.Range (0, potentialStartTiles.Count - 1)]) as TrackChunk;
		startChunk.transform.SetParent (this.transform);
		startChunk.name = "Start Track Segment";
		TrackChunk currentChunk = startChunk;
		Direction previousDirection = startChunk.GetOpenGates () [0].GateDirection;
		for (int i = 0; i < trackLength; i++) {
			//Generate a new piece that matches up with the other pieces.
			Direction opposingDirection = previousDirection.GetOpposite ();
			List<TrackChunk> availableTrackParts = new List<TrackChunk> ();
			foreach (TrackChunk obj in trackSegmentPrefabs) {
				if (obj != null && obj.GetOpenGates().Count > 1 && obj.GetGateForDirection (opposingDirection) != GateSize.CLOSED) {
					availableTrackParts.Add (obj);
				}
			}
			if (availableTrackParts.Count == 0) {
				Debug.LogError ("No available parts.");
				return;
			}
			Debug.Log ("Parts available for segment " + i + ": " + availableTrackParts.Count);
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

		List<TrackChunk> potentialEndTiles = trackSegmentPrefabs.Where ((chunk) => chunk != null && chunk.canBeEnd && chunk.GetGateForDirection(previousDirection.GetOpposite ()) != GateSize.CLOSED).ToList ();
		if (potentialEndTiles.Count == 0) {
			Debug.Log ("Unable to end track - no potential end tiles.");
			return;
		}
		TrackChunk endChunk = Instantiate(potentialEndTiles [Random.Range (0, potentialEndTiles.Count - 1)]) as TrackChunk;
		endChunk.PositionSideAtPoint (currentChunk.GetGatePosition (previousDirection), endChunk.GetOpenGates () [0].GateDirection);
		endChunk.name = "End Track Segment";
		endChunk.transform.SetParent (this.transform);
	}
}
