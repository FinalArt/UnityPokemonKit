using UnityEngine;
using System.Collections;

/**
 * Handle player's movement and direction.
 * @Require : The "Character Controller" component on the "Player" prefab.
 * @Require : The "PlayerOverworld" tag on the "Overworld" game object of the "Player" prefab.
 * @Info : Change keyboard shortcuts in goingXX() functions at bottom.
 * @Author : FinalArt (08/2015)
 */
public class PlayerController : MonoBehaviour {

	// Parameters in component
	public float speed = 2.0F;
	public bool useSemiCardinalMovements = true;
	// -----------------------

	private readonly float GRAVITY = 20.0F;
	private enum Direction : int {	UP=0, UP_RIGHT=45, UP_LEFT=315,
									DOWN=180, DOWN_RIGHT=135, DOWN_LEFT=225,
									RIGHT=90, LEFT=270, NONE=-1}; //Degree values

	private CharacterController controller;
	private GameObject player;

	void Start() {
		this.controller = GetComponent<CharacterController>();
		this.player = GameObject.FindWithTag("PlayerOverworld");
		if (this.player == null) {
			Debug.LogError("Tag 'PlayerOverworld' not set up on player's overworld (or player doesn't exist).");
			this.enabled = false;
		}
	}
	
	void Update() {
		this.rotatePlayer ();
		this.movePlayer ();
	}

	// Rotate just the "Overworld" game object
	void rotatePlayer() {
		if (this.player != null) {
			Direction direction = this.getDirection();
			if ((int)direction >= 0) {
				this.player.transform.eulerAngles = new Vector3 (0, (int)direction, 0);
			}
		}
	}

	// Move the entire "Player" game object ("Overworld" + "MainCamera")
	void movePlayer() 
	{
		if (this.controller != null) {
			Vector3 moveDirection = Vector3.zero;
			if (this.controller.isGrounded) {
				float horizontalValue = Input.GetAxis ("Horizontal");
				float verticalValue = Input.GetAxis ("Vertical");
				if (this.useSemiCardinalMovements) {
					moveDirection = new Vector3 (horizontalValue, 0, verticalValue);
				} else if (verticalValue != 0) {
					moveDirection = new Vector3 (0, 0, verticalValue);
				} else if (horizontalValue != 0) {
					moveDirection = new Vector3 (horizontalValue, 0, 0);
				}
				moveDirection *= speed;
			}
			moveDirection.y -= GRAVITY * Time.deltaTime;
			this.controller.Move (moveDirection * Time.deltaTime);
		}
	}

	Direction getDirection() {
		if (this.goingUp ()) {
			if (this.goingRight()) {
				return (this.useSemiCardinalMovements) ? Direction.UP_RIGHT : Direction.UP;
			} else if (this.goingLeft()) {
				return (this.useSemiCardinalMovements) ? Direction.UP_LEFT : Direction.UP;
			} else {
				return Direction.UP;
			}
		}
		if (this.goingDown ()) {
			if (this.goingRight()) {
				return (this.useSemiCardinalMovements) ? Direction.DOWN_RIGHT : Direction.DOWN;
			} else if (this.goingLeft()) {
				return (this.useSemiCardinalMovements) ? Direction.DOWN_LEFT : Direction.DOWN;
			} else {
				return Direction.DOWN;
			}
		}
		if (this.goingLeft ()) {
			return Direction.LEFT; //Semi cardinals has been already checked
		}
		if (this.goingRight ()) {
			return Direction.RIGHT;
		}
		return Direction.NONE;
	}

	bool goingUp() { 
		return (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow));
	}

	bool goingRight() { 
		return (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow));
	}

	bool goingDown() { 
		return (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow));
	}

	bool goingLeft() { 
		return (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow));
	}

}