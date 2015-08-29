using UnityEngine;
using System.Collections;

/**
 * Make character speak with custom message and GUI skin
 * @Require : The "PlayerOverworld" tag on the "Overworld" game object of the "Player" prefab.
 * @Info : Change keyboard shortcuts for "Submit" in Unity with Edit > Project Settings > Input > Submit
 * @Author : FinalArt (08/2015)
 */
public class Speak : MonoBehaviour {

	[TextArea(3,10)]
	public string message = "Un texte";
	public GUISkin skin;
	public int lineSize = 25;
	
	private float MIN_DISTANCE = 1f;
	private int LINES_AMOUNT = 2;

	private Transform playerTransform; 
	private Rect messageBox;
	private string[] lines;
	private int lineIndex;
	private bool showText;

	void Start() {
		this.playerTransform = GameObject.FindWithTag("PlayerOverworld").transform;
		if (this.playerTransform == null) {
			Debug.LogError ("Tag 'PlayerOverworld' not set up on player's overworld (or player doesn't exist).");
			this.enabled = false;
		} 
		this.messageBox = new Rect ((Screen.width - skin.box.fixedWidth) / 2, (Screen.height - skin.box.fixedHeight) / 1.1f, 
		                            skin.box.fixedWidth, skin.box.fixedHeight);
		this.lines = PlayerDialogServices.convertToLines(this.message, this.lineSize);
		this.hideText();
	}
	
	void Update() {
		if (Vector3.Distance(transform.position, playerTransform.position) <= MIN_DISTANCE) {
			if (Input.GetButtonDown("Submit")) { // "Space" or "Numerical Pad Enter" key
				if (!showText) {
					showText = true;
				} else {
					this.lineIndex += this.LINES_AMOUNT;
					if (this.lineIndex >= this.lines.Length) {
						this.hideText();
					}
				}
			}
		} else {
			this.hideText();
		}
	}
	
	void OnGUI() {
		if (showText) {
			GUI.skin = skin;
			GUI.Box(messageBox, PlayerDialogServices.getWrappedLines(this.lines, this.LINES_AMOUNT, this.lineIndex));
		}
	}

	private void hideText() {
		showText = false;
		this.lineIndex = 0;
	}
}