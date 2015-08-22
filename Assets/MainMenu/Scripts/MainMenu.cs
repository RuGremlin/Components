using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture backgroundTexture;
	public float startButtonWidth = Screen.width * .5f;
	public float startButtonHeight = Screen.height * .1f;

	void OnGUI() {
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);

		if (GUI.Button (new Rect (Screen.width * .5f - startButtonWidth * .5f, Screen.height * .5f - startButtonHeight * .5f, startButtonWidth, startButtonHeight), "Play Game")) {
			Application.LoadLevel("Level1");
		}
	}
}
