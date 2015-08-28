using UnityEngine;
using System.Collections;

/**
 * Handle player's movement and direction.
 * @Require : The "Character Controller" component on the "Player" prefab.
 * @Require : The "PlayerOverworld" tag on the "Overworld" game object of the "Player" prefab.
 * @Info : Change keyboard shortcuts in "UPK/Scripts/Tools/UserInputs.cs" in "goXXX()" functions.
 * @Author : FinalArt (08/2015)
 */
public class PlayerController : MonoBehaviour {

	// Parameters in component
	public float speed = 2.0F;
	public bool useSemiCardinalMovements = true;
	// -----------------------

	private readonly float GRAVITY = 20.0F;
	private CharacterController controller;
	private GameObject player;

	public void Start() {
		this.controller = GetComponent<CharacterController>();
		this.player = GameObject.FindWithTag("PlayerOverworld");
		if (this.player == null) {
			Debug.LogError("Tag 'PlayerOverworld' not set up on player's overworld (or player doesn't exist).");
			this.enabled = false;
		}
	}

	public void Update() {
		this.rotatePlayer ();
		this.movePlayer ();
	}

	// Rotate just the "Overworld" game object
	private void rotatePlayer() {
		if (this.player != null) {
			int angle = PlayerMovementServices.computeRotation(UserInputs.goUp(), UserInputs.goRight(), UserInputs.goDown(), UserInputs.goLeft(), 
			                                                   this.useSemiCardinalMovements);
			if (angle >= 0) {
				this.player.transform.eulerAngles = new Vector3 (0, angle, 0);
			}
		}
	}

	// Move the entire "Player" game object ("Overworld" + "MainCamera")
	private void movePlayer() 
	{
		if (this.controller != null) {
			Vector3 moveDirection = Vector3.zero;
			if (this.controller.isGrounded) {
				moveDirection = PlayerMovementServices.computeMove(Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"),
				                                                   this.useSemiCardinalMovements, this.speed);
			}
			moveDirection.y -= GRAVITY * Time.deltaTime;
			this.controller.Move (moveDirection * Time.deltaTime);
		}
	}

}