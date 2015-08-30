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

	[Header("> Simple settings")]
	[TextArea(3,10)]
	public string message = "Un texte";
	public GUISkin skin;

	[Header("> Advanced settings")]
	public Texture2D arrowImage;
	public int lineSize = 56;

	private readonly float MESSAGE_BOX_X_ALIGNMENT = 2.0f; // 2.0 is centered
	private readonly float MESSAGE_BOX_Y_ALIGNMENT = 1.1f; // 2.0 is centered
	private readonly string INPUT_KEY = "Submit"; // "Space" or "Numerical Pad Enter" key
	private readonly int LINES_AMOUNT = 2;
	private readonly int MAX_ANGLE = 45;
	private readonly float MIN_DISTANCE = 0.6f;
	private readonly int ARROW_SHIFT_DELAY = 50;
	private readonly int ARROW_X_POSITION = 356;
	private readonly int ARROW_Y_POSITION = 44;
	private readonly int ARROW_Y_SHIFT = 2;

	private GameObject player; 
	private PlayerController playerController;
	
	private Rect messageBox;
	private string[] lines;
	private int lineIndex;
	private bool showText;

	private Rect arrowBox;
	private int arrowTimer;
	private bool arrowOnTop;
	private bool showArrow;

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

		this.messageBox = new Rect ((Screen.width - this.skin.box.fixedWidth) / MESSAGE_BOX_X_ALIGNMENT, 
		                            (Screen.height - this.skin.box.fixedHeight) / MESSAGE_BOX_Y_ALIGNMENT, 
		                            this.skin.box.fixedWidth, this.skin.box.fixedHeight);
		this.arrowBox = new Rect (this.messageBox.x + ARROW_X_POSITION, this.messageBox.y + ARROW_Y_POSITION, 
		                          this.arrowImage.width, this.arrowImage.height);

		this.lines = PlayerDialogServices.convertToLines(this.message, this.lineSize);
		this.lineIndex = 0;
		this.showText = false;
		this.arrowTimer = ARROW_SHIFT_DELAY;
		this.arrowOnTop = true;
		this.showArrow = false;
	}
	
	void Update() {
		if (canSpeak()) {
			if (Input.GetButtonDown(INPUT_KEY)) {
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
			shiftTheArrow();
		}
	}

	private void enableDialog() {
		this.showText = true;
		this.arrowTimer = ARROW_SHIFT_DELAY;
		this.arrowOnTop = true;
		this.showArrow = (this.lines.Length > LINES_AMOUNT);
		this.playerController.enabled = false;
	}

	private void disableDialog() {
		this.showText = false;
		this.lineIndex = 0;
		this.arrowTimer = ARROW_SHIFT_DELAY;
		this.showArrow = false;
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

	private void shiftTheArrow() {
		if (this.showArrow) {
			this.arrowTimer--;
			if (this.arrowTimer == 0) {
				this.arrowOnTop = !this.arrowOnTop;
				this.arrowBox.y = this.messageBox.y + ARROW_Y_POSITION + ((this.arrowOnTop) ? 0 : ARROW_Y_SHIFT);
				this.arrowTimer = ARROW_SHIFT_DELAY;
			}
			GUI.DrawTexture (this.arrowBox, this.arrowImage, ScaleMode.ScaleToFit, true, 1.0f);
		}
	}
}