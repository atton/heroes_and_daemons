using UnityEngine;
using System;
using System.Collections;
using GameSystem;
using GameSystem.SettingDefinition;

public class CharacterSetting : MonoBehaviour {

	private Rect exitMenu;
	private Rect characterSelectMenu;
	private Rect skillSelectMenu;
	
	private const int exitMenuID            = 0;
	private const int characterSelectMenuID = 1;
	private const int skillSelectMenuID     = 2;

	private const int kRectSpace  = 20;
	private const int kRectHeight = 100;

	private const string kMainMenuName = "OnlineMatching";

	void Awake() {
		/* top */
		characterSelectMenu = new Rect(kRectSpace, kRectSpace,
		                               Screen.width - kRectSpace*2, kRectHeight);

		/* middle */
		skillSelectMenu    = new Rect(kRectSpace, kRectSpace*3 + kRectHeight,
		                              Screen.width - kRectSpace*2, kRectHeight);

		/* bottom */
		exitMenu            = new Rect(kRectSpace, Screen.height - (kRectSpace*2 + kRectHeight),
		                               Screen.width - kRectSpace*2, kRectHeight);
	}

	void OnGUI() {
		exitMenu            = GUILayout.Window(exitMenuID, exitMenu, makeExitMenu, "Back to Main Menu");
		characterSelectMenu = GUILayout.Window(characterSelectMenuID, characterSelectMenu,
		                                       makeCharacterSelectMenu, "Please Select Your Character");
		skillSelectMenu     = GUILayout.Window(skillSelectMenuID, skillSelectMenu,
		                                       makeSkilSelectMenu, "Please Select Your Character Skills");
	}

	/* methods for create menu */

	void makeExitMenu(int id) {
		GUILayout.BeginHorizontal();

		GUILayout.Label("Selected Character : " + GlobalSettings.Setting.Character.ToString());

		GUILayout.BeginVertical();
		GUILayout.Label("Selected SkillA    : " + GlobalSettings.Setting.SkillA.ToString());
		GUILayout.Label("Selected SkillB    : " + GlobalSettings.Setting.SkillB.ToString());
		GUILayout.Label("Selected SkillC    : " + GlobalSettings.Setting.SkillC.ToString());
		GUILayout.EndVertical();

		GUILayout.EndHorizontal();

		if (GUILayout.Button("exit")) Application.LoadLevel(kMainMenuName);

	}

	void makeCharacterSelectMenu(int id) {
		GUILayout.BeginHorizontal();

		if (GUILayout.Button("Slayer")) GlobalSettings.Setting.Character = Character.Slayer;
		if (GUILayout.Button("Golem"))  GlobalSettings.Setting.Character = Character.Golem;

		GUILayout.EndHorizontal();
	}

	void makeSkilSelectMenu(int id) {
		Skill selectedSkillA           = selectSkillFromButtons("SkillA");
		selectedSkillA                 = (selectedSkillA == Skill.None) ? GlobalSettings.Setting.SkillA : selectedSkillA;
		GlobalSettings.Setting.SkillA  = selectedSkillA;

		Skill selectedSkillB           = selectSkillFromButtons("SkillB");
		selectedSkillB                 = (selectedSkillB == Skill.None) ? GlobalSettings.Setting.SkillB : selectedSkillB;
		GlobalSettings.Setting.SkillB  = selectedSkillB;
		
		Skill selectedSkillC           = selectSkillFromButtons("SkillC");
		selectedSkillC                 = (selectedSkillC == Skill.None) ? GlobalSettings.Setting.SkillC : selectedSkillC;
		GlobalSettings.Setting.SkillC  = selectedSkillC;
	}

	/* helpers */
	private Skill selectSkillFromButtons(string skillLabel) {
		GUILayout.BeginHorizontal();

		GUILayout.Label(skillLabel + " : ");

		foreach (Skill s in Enum.GetValues(typeof(Skill))) {
			if (s == Skill.None) continue;
			if (GUILayout.Button(s.ToString())) return s;
		}


		GUILayout.EndHorizontal();
		return Skill.None;
	}
		
}
