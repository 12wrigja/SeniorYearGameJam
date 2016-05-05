using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackChunk : MonoBehaviour
{

	public float width = 50f;
	public float height = 50f;

	public GateSize TopGate;
	public GateSize BottomGate;
	public GateSize LeftGate;
	public GateSize RightGate;
	public GateSize ForwardGate;
	public GateSize BackGate;

	public GateSize GetGateForDirection (Direction dir)
	{
		switch (dir) {
		case Direction.TOP:
			return TopGate;
		case Direction.BOTTOM:
			return BottomGate;
		case Direction.LEFT:
			return LeftGate;
		case Direction.RIGHT:
			return RightGate;
		case Direction.FORWARD:
			return ForwardGate;
		case Direction.BACK:
			return BackGate;
		default:
			return GateSize.CLOSED;
		}
	}

	private Vector3 GateSizeScale (GateSize size, Direction dir)
	{
		switch (size) {
		case GateSize.SMALL:
			if (dir == Direction.FORWARD || dir == Direction.BACK) {
				return new Vector3 (8, 4, 1);
			} else if (dir == Direction.LEFT || dir == Direction.RIGHT) {
				return new Vector3 (1, 4, 8);
			} else {
				return new Vector3 (8, 1, 4);
			}
		case GateSize.MEDIUM:
			if (dir == Direction.FORWARD || dir == Direction.BACK) {
				return new Vector3 (16, 8, 1);
			} else if (dir == Direction.LEFT || dir == Direction.RIGHT) {
				return new Vector3 (1, 8, 16);
			} else {
				return new Vector3 (16, 1, 8);
			}
		case GateSize.LARGE:
			if (dir == Direction.FORWARD || dir == Direction.BACK) {
				return new Vector3 (26, 16, 1);
			} else if (dir == Direction.LEFT || dir == Direction.RIGHT) {
				return new Vector3 (1, 16, 26);
			} else {
				return new Vector3 (26, 1, 16);
			}
		default:
			return new Vector3 (1, 1, 1);
		}
	}

	void OnDrawGizmosSelected ()
	{
		if (TopGate != GateSize.CLOSED) {
			Gizmos.DrawWireCube (GetGatePosition (Direction.TOP), GateSizeScale (TopGate, Direction.TOP));
		}
		if (BottomGate != GateSize.CLOSED) {
			Gizmos.DrawWireCube (GetGatePosition (Direction.BOTTOM), GateSizeScale (BottomGate, Direction.BOTTOM));
		}
		if (LeftGate != GateSize.CLOSED) {
			Gizmos.DrawWireCube (GetGatePosition (Direction.LEFT), GateSizeScale (LeftGate, Direction.LEFT));
		}
		if (RightGate != GateSize.CLOSED) {
			Gizmos.DrawWireCube (GetGatePosition (Direction.RIGHT), GateSizeScale (RightGate, Direction.RIGHT));
		}
		if (ForwardGate != GateSize.CLOSED) {
			Gizmos.DrawWireCube(GetGatePosition (Direction.FORWARD), GateSizeScale (ForwardGate, Direction.FORWARD));
		}
		if (BackGate != GateSize.CLOSED) {
			Gizmos.DrawWireCube (GetGatePosition (Direction.BACK), GateSizeScale (BackGate, Direction.BACK));
		}
	}

	public Vector3 GetGatePosition (Direction dir)
	{
		switch (dir) {
		case Direction.TOP:
			return this.transform.position + (transform.up * 24.5f);
		case Direction.BOTTOM:
			return this.transform.position + (transform.up * -24.5f);
		case Direction.LEFT:
			return this.transform.position + (transform.right * -24.5f);
		case Direction.RIGHT:
			return this.transform.position + (transform.right * 24.5f);
		case Direction.FORWARD:
			return this.transform.position + (transform.forward * 24.5f);
		case Direction.BACK:
			return this.transform.position + (transform.forward * -24.5f);
		default:
			return this.transform.position;
		}
	}

	public List<PositionedGate> GetOpenGates ()
	{
		List<PositionedGate> openGates = new List<PositionedGate> ();
		if (TopGate != GateSize.CLOSED) {
			openGates.Add (new PositionedGate (Direction.TOP, TopGate));
		}
		if (BottomGate != GateSize.CLOSED) {
			openGates.Add (new PositionedGate (Direction.BOTTOM, BottomGate));
		}
		if (LeftGate != GateSize.CLOSED) {
			openGates.Add (new PositionedGate (Direction.LEFT, LeftGate));
		}
		if (RightGate != GateSize.CLOSED) {
			openGates.Add (new PositionedGate (Direction.RIGHT, RightGate));
		}
		if (ForwardGate != GateSize.CLOSED) {
			openGates.Add (new PositionedGate (Direction.FORWARD, ForwardGate));
		}
		if (BackGate != GateSize.CLOSED) {
			openGates.Add (new PositionedGate (Direction.BACK, BottomGate));
		}
		return openGates;
	}

	public Direction getExitDirection (Direction enterDirection)
	{
		List<PositionedGate> gates = GetOpenGates ();
		foreach (PositionedGate gate in gates) {
			if (gate.GateDirection != enterDirection) {
				return gate.GateDirection;
			}
		}
		return Direction.UNKNOWN;
	}

	public void PositionSideAtPoint (Vector3 Point, Direction dir)
	{
		Vector3 pointTranslationVector = this.transform.position - GetGatePosition (dir);
		this.transform.position = Point + pointTranslationVector;
	}
}

public struct PositionedGate
{
	public Direction GateDirection;
	public GateSize Size;

	public PositionedGate (Direction dir, GateSize size)
	{
		this.GateDirection = dir;
		this.Size = size;
	}
}
