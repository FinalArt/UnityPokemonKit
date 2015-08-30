using UnityEngine;
using System.Collections;

/**
 * Make character speak with player
 * Inherits of 'Message' class
 * @Author : FinalArt (08/2015)
 */
public class Speak : Message {

	protected override void look() {
		Vector3 speakerTarget = new Vector3(this.player.transform.position.x, this.transform.position.y, this.player.transform.position.z);
		this.transform.LookAt(speakerTarget);
		Vector3 playerTarget = new Vector3(this.transform.position.x, this.player.transform.position.y, this.transform.position.z);
		this.player.transform.LookAt(playerTarget);
	}

}