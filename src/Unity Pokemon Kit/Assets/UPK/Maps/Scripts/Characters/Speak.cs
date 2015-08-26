using UnityEngine;
using System.Collections;

/**
 * Make character speak with custom message and GUI skin
 * @Require : The "PlayerOverworld" tag on the "Overworld" game object of the "Player" prefab.
 * @Info : Change keyboard shortcuts for "Submit" in Unity with Edit > Project Settings > Input > Submit
 * @Author : FinalArt (08/2015)
 */
public class Speak : MonoBehaviour {

	public string message = "Empty text"; 
	public GUISkin skin;

	private float MIN_DISTANCE = 1f;

	private Transform playerTransform; 
	private bool showText;

	void Start() {
		this.playerTransform = GameObject.FindWithTag("PlayerOverworld").transform;
		if (this.playerTransform == null) {
			Debug.LogError ("Tag 'PlayerOverworld' not set up on player's overworld (or player doesn't exist).");
			this.enabled = false;
		} else {
			this.showText = false;
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
			GUI.skin = skin;
			Rect messageBox = new Rect ((Screen.width - skin.box.fixedWidth) / 2, (Screen.height - skin.box.fixedHeight) / 1.1f, 
			                            skin.box.fixedWidth, skin.box.fixedHeight);
			GUI.Box(messageBox, message);
		}
	}
}