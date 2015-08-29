using UnityEngine;
using System.Collections;

/**
 * Make character speak with custom message and GUI skin
 * @Require : The "Character Controller" component on the "Player" prefab.
 * @Require : The "PlayerOverworld" tag on the "Overworld" game object of the "Player" prefab.
 * @Info : Change keyboard shortcuts for "Submit" in Unity with Edit > Project Settings > Input > Submit
 * @Author : FinalArt (08/2015)
 */
public class Speak : MonoBehaviour {

	[TextArea(3,10)]
	public string message = "Un texte";
	public GUISkin skin;
	public int lineSize = 25;
	
	private float MIN_DISTANCE = 0.6f;
	private int MAX_ANGLE = 45;
	private int LINES_AMOUNT = 2;

	private GameObject player; 
	private PlayerController playerController;

	private Rect messageBox;
	private string[] lines;
	private int lineIndex;
	private bool showText;

	void Start() {
		this.player = GameObject.FindWithTag("PlayerOverworld");
		if (this.player == null) {
			Debug.LogError("Tag 'PlayerOverworld' not set up on player's overworld (or player doesn't exist).");
			this.enabled = false;
		}
		this.playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		if (this.playerController  == null) {
			Debug.LogError("Script 'PlayerController' not set up on player's overworld (or tag 'Player' not set up on player).");
			this.enabled = false;
		}
		this.messageBox = new Rect ((Screen.width - this.skin.box.fixedWidth) / 2, (Screen.height - this.skin.box.fixedHeight) / 1.1f, 
		                            this.skin.box.fixedWidth, this.skin.box.fixedHeight);
		this.lines = PlayerDialogServices.convertToLines(this.message, this.lineSize);
		disableDialog();
	}
	
	void Update() {
		if (canSpeak()) {
			if (Input.GetButtonDown("Submit")) { // "Space" or "Numerical Pad Enter" key
				lookInTheEyes();
				if (this.showText) {
					this.lineIndex += this.LINES_AMOUNT;
					if (this.lineIndex >= this.lines.Length) {
						disableDialog();
					}
				} else {
					enableDialog();
				}
			}
		}
	}

	void OnGUI() {
		if (showText) {
			GUI.skin = this.skin;
			GUI.Box(this.messageBox, PlayerDialogServices.getWrappedLines(this.lines, this.LINES_AMOUNT, this.lineIndex));
		}
	}

	private void enableDialog() {
		this.showText = true;
		this.playerController.enabled = false;
	}

	private void disableDialog() {
		this.showText = false;
		this.lineIndex = 0;
		this.playerController.enabled = true;
	}

	private bool canSpeak() {
		return ((Vector3.Distance (this.transform.position, this.player.transform.position) <= MIN_DISTANCE)
				&& (Vector3.Angle (this.player.transform.forward, this.transform.position - this.player.transform.position) < MAX_ANGLE));
	}
	
	private void lookInTheEyes() {
		Vector3 speakerTarget = new Vector3(this.player.transform.position.x, this.transform.position.y, this.player.transform.position.z);
		this.transform.LookAt(speakerTarget);
		Vector3 playerTarget = new Vector3(this.transform.position.x, this.player.transform.position.y, this.transform.position.z);
		this.player.transform.LookAt(playerTarget);
	}

}