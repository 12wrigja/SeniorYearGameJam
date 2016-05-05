using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class TrackChunk : MonoBehaviour {

	public float width = 50f;
	public float height = 50f;

	public GateSize TopGate;
	public GateSize BottomGate;
	public GateSize LeftGate;
	public GateSize RightGate;
	public GateSize ForwardGate;
	public GateSize BackGate;

	public GateSize GetGateForDirection(Direction dir){
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

	void OnDrawGizmosSelected(){
		Vector3 cubeSize = new Vector3 (1, 1, 1);
		if (TopGate != GateSize.CLOSED) {
			Gizmos.DrawCube (GetGatePosition(Direction.TOP), cubeSize);
		}
		if (BottomGate != GateSize.CLOSED) {
			Gizmos.DrawCube (GetGatePosition(Direction.BOTTOM), cubeSize);
		}
		if (LeftGate != GateSize.CLOSED) {
			Gizmos.DrawCube (GetGatePosition(Direction.LEFT), cubeSize);
		}
		if (RightGate != GateSize.CLOSED) {
			Gizmos.DrawCube (GetGatePosition(Direction.RIGHT), cubeSize);
		}
		if (ForwardGate != GateSize.CLOSED) {
			Gizmos.DrawCube (GetGatePosition(Direction.FORWARD), cubeSize);
		}
		if (BackGate != GateSize.CLOSED) {
			Gizmos.DrawCube (GetGatePosition(Direction.BACK), cubeSize);
		}
	}

	public Vector3 GetGatePosition(Direction dir){
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

	public List<PositionedGate> GetOpenGates(){
		List<PositionedGate> openGates = new List<PositionedGate> ();
		if (TopGate != GateSize.CLOSED) {
			openGates.Add(new PositionedGate(Direction.TOP,TopGate));
		}
		if (BottomGate != GateSize.CLOSED) {
			openGates.Add(new PositionedGate(Direction.BOTTOM,BottomGate));
		}
		if (LeftGate != GateSize.CLOSED) {
			openGates.Add(new PositionedGate(Direction.LEFT,LeftGate));
		}
		if (RightGate != GateSize.CLOSED) {
			openGates.Add(new PositionedGate(Direction.RIGHT,RightGate));
		}
		if (ForwardGate != GateSize.CLOSED) {
			openGates.Add(new PositionedGate(Direction.FORWARD,ForwardGate));
		}
		if (BackGate != GateSize.CLOSED) {
			openGates.Add(new PositionedGate(Direction.BACK,BottomGate));
		}
		return openGates;
	}

	public Direction getExitDirection(Direction enterDirection){
		List<PositionedGate> gates = GetOpenGates ();
		foreach (PositionedGate gate in gates) {
			if (gate.GateDirection != enterDirection) {
				return gate.GateDirection;
			}
		}
		return Direction.UNKNOWN;
	}

	public void PositionSideAtPoint(Vector3 Point, Direction dir){
		Vector3 pointTranslationVector = this.transform.position - GetGatePosition(dir);
		this.transform.position = Point + pointTranslationVector;
	}
}

public struct PositionedGate {
	public Direction GateDirection;
	public GateSize Size;

	public PositionedGate(Direction dir, GateSize size){
		this.GateDirection = dir;
		this.Size = size;
	}
}
