using UnityEngine;
using System;

/**
 * Utility to catch user inputs
 * @Author : FinalArt (08/2015)
 */
public class UserInputs
{

	public static bool goUp() { 
		return (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow));
	}
	
	public static bool goRight() { 
		return (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow));
	}
	
	public static bool goDown() { 
		return (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow));
	}
	
	public static bool goLeft() { 
		return (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow));
	}

}