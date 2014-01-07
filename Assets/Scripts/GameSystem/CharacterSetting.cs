using UnityEngine;
using System.Collections;
using GameSystem;
using GameSystem.SettingDefinition;

public class CharacterSetting : MonoBehaviour {

	private Rect exitMenu;
	private Rect characterSelectMenu;
	
	private const int exitMenuID            = 0;
	private const int characterSelectMenuID = 1;

	private const int kRectSpace  = 20;
	private const int kRectHeight = 100;

	private const string kMainMenuName = "OnlineMatching";

	void Awake() {
		/* top */
		characterSelectMenu = new Rect(kRectSpace, kRectSpace,
		                               Screen.width - kRectSpace*2, kRectHeight);
		/* bottom */
		exitMenu            = new Rect(kRectSpace, Screen.height - (kRectSpace + kRectHeight),
		                               Screen.width - kRectSpace*2, kRectHeight);
	}

	void OnGUI() {
		exitMenu            = GUILayout.Window(exitMenuID, exitMenu, makeExitMenu, "Back to Main Menu");
		characterSelectMenu = GUILayout.Window(characterSelectMenuID, characterSelectMenu,
		                                       makeCharacterSelectMenu, "Please Select Your Character");
	}

	/* methods for create menu */

	void makeExitMenu(int id) {
		if (GUILayout.Button("exit")) Application.LoadLevel(kMainMenuName);

		GUILayout.Label("Selected Character : " + GlobalSettings.Setting.Character.ToString());
	}

	void makeCharacterSelectMenu(int id) {
		if (GUILayout.Button("Slayer"))  GlobalSettings.Setting.Character = Character.Slayer;
		if (GUILayout.Button("DMascot")) GlobalSettings.Setting.Character = Character.DMascot;
	}
		
}
