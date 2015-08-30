using UnityEngine;
using System;

/**
 * @Authors : FinalArt (08/2015)
 */
public class PlayerMovementServices {
	
	public static Vector3 computeMove(float horizontalInput, float verticalInput, bool useSemiCardinalMovements, float speed) {
		Vector3 move = Vector3.zero;
		if (useSemiCardinalMovements) {
			move = new Vector3 (horizontalInput, 0, verticalInput);
		} else if (verticalInput != 0) { // Priorize vertical on horizontal input
			move = new Vector3 (0, 0, verticalInput);
		} else if (horizontalInput != 0) {
			move = new Vector3 (horizontalInput, 0, 0);
		}
		return move *= speed;
	}
	
	public static int computeRotation(bool goingUp, bool goingRight, bool goingDown, bool goingLeft, bool useSemiCardinalMovements) {
		if (useSemiCardinalMovements) {
			if (goingUp) {
				return (int) (goingRight ? CharacterDirectionDegrees.UP_RIGHT :
				              	goingLeft ? CharacterDirectionDegrees.UP_LEFT : CharacterDirectionDegrees.UP);
			} else if (goingDown) {
				return (int) (goingRight ? CharacterDirectionDegrees.DOWN_RIGHT :
				              	goingLeft ? CharacterDirectionDegrees.DOWN_LEFT : CharacterDirectionDegrees.DOWN);
			} else if (goingLeft) {
				return (int) CharacterDirectionDegrees.LEFT; //Semi cardinals has been already checked
			} else if (goingRight) {
				return (int) CharacterDirectionDegrees.RIGHT;
			} else {
				return (int) CharacterDirectionDegrees.NONE;
			}
		} else {
			return (int) (goingUp ? CharacterDirectionDegrees.UP : 
			             	 goingRight ? CharacterDirectionDegrees.RIGHT :
			              		goingDown ? CharacterDirectionDegrees.DOWN : 
			              			goingLeft ? CharacterDirectionDegrees.LEFT : CharacterDirectionDegrees.NONE);
		}
	}

}