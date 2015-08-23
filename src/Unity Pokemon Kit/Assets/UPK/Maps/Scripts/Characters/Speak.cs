using UnityEngine;
using System.Collections;

/**
 * Make character speak with custom message and box
 * @Require : The "PlayerOverworld" tag on the "Overworld" game object of the "Player" prefab.
 * @Info : Change keyboard shortcuts for "Submit" in Unity with Edit > Project Settings > Input > Submit
 * @Author : FinalArt (08/2015)
 */
public class Speak : MonoBehaviour {

	public string message = "Empty text"; 
	public Rect boxSize = new Rect(0.1f, 0.7f, 0.8f, 0.2f); 

	private float MIN_DISTANCE = 1f;

	private Transform playerTransform; 
	private Rect messageBox;
	private bool showText;

	void Start() {
		this.playerTransform = GameObject.FindWithTag("PlayerOverworld").transform;
		if (this.playerTransform == null) {
			Debug.LogError ("Tag 'PlayerOverworld' not set up on player's overworld (or player doesn't exist).");
			this.enabled = false;
		} else {
			this.showText = false;
			this.messageBox = new Rect (Screen.width * boxSize.x, Screen.height * boxSize.y, 
			                            Screen.width * boxSize.width, Screen.height * boxSize.height);
		}
	}
	
	void Update() {
		if (Vector3.Distance(transform.position, playerTransform.position) <= MIN_DISTANCE) {
			if (Input.GetButtonDown("Submit")) { // "Space" or "Numerical Pad Enter" key
				showText = !showText; 
			}
		} else {
			showText = false;
		}
	}
	
	void OnGUI() {
		if (showText) {
			GUI.Box(messageBox, message);
		}
	}
}