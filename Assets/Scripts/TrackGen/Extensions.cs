using UnityEngine;
using System.Collections;

public static class Extensions {

	public static Direction GetOpposite(this Direction dir){
		switch (dir) {
		case Direction.TOP:
			return Direction.BOTTOM;
		case Direction.BOTTOM:
			return Direction.TOP;
		case Direction.LEFT:
			return Direction.RIGHT;
		case Direction.RIGHT:
			return Direction.LEFT;
		case Direction.FORWARD:
			return Direction.BACK;
		case Direction.BACK:
			return Direction.FORWARD;
		default:
			return Direction.UNKNOWN;
		}
	}
}
