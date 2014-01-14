using System;
using UnityEngine;
using CharacterInterface;
using CharacterSkill;

namespace GameSystem {
	public class PlayerController {
		/* definitions */
		private const KeyCode KeySkillA = KeyCode.Z;
		private const KeyCode KeySkillB = KeyCode.X;
		private const KeyCode KeySkillC = KeyCode.C;
		private const KeyCode KeyJump   = KeyCode.Space;

		/* Skills */
		private SkillControl skillA;
		private SkillControl skillB;
		private SkillControl skillC;

		public PlayerController(CharacterParameter characterParameter) {
			skillA = new SkillControl(characterParameter, GlobalSettings.Setting.SkillA);
			skillB = new SkillControl(characterParameter, GlobalSettings.Setting.SkillB);
			skillC = new SkillControl(characterParameter, GlobalSettings.Setting.SkillC);
		}

		/* character control method */
		public void UpdateCharacterFromInput(IControllable character) {
			moveFromInput(character);
			jumpFromInput(character);

			useSkillFromInput(character, skillA, KeySkillA);
			useSkillFromInput(character, skillB, KeySkillB);
			useSkillFromInput(character, skillC, KeySkillC);
			inclementCoolFrameAllSkills();
		}

		/* character control helper methods */
		private void moveFromInput(IControllable character) {
			float   upOrDown    = Input.GetAxis("Horizontal");
			float   rightOrLeft = Input.GetAxis("Vertical");
			Vector3 moveVector  = new Vector3(-rightOrLeft, 0, upOrDown);

			character.Move(moveVector);
		}

		private void jumpFromInput(IControllable character) {
			if (Input.GetKeyUp(KeyJump)) character.Jump();
		}

		private void useSkillFromInput(IControllable character, SkillControl skillControll, KeyCode skillKey) {
			if (skillControll.IsSkillAvailable() && Input.GetKeyUp(skillKey)) {
				if (character.UseSkill(skillControll.Skill)) skillControll.SkillUsed();
			}
		}

		private void inclementCoolFrameAllSkills() {
			skillA.InclementCoolingFrame();
			skillB.InclementCoolingFrame();
			skillC.InclementCoolingFrame();
		}

		/* public methods */

		/* show skill status */
		private const int kSpace = 25;
		public void OnGUI() {

			GUI.Label(new Rect(kSpace , Screen.height - kSpace*3, Screen.width - kSpace*2, kSpace),
			          "SkillA : " + labelFromSkillControl(skillA));
			GUI.Label(new Rect(kSpace , Screen.height - kSpace*2, Screen.width - kSpace*2, kSpace),
			          "SkillB : " + labelFromSkillControl(skillB));
			GUI.Label(new Rect(kSpace , Screen.height - kSpace*1, Screen.width - kSpace*2, kSpace),
			          "SkillC : " + labelFromSkillControl(skillC));
		}

		private string labelFromSkillControl(SkillControl skillControl) {
			string str = "";

			str += skillControl.Skill.ToString();
			str += " ";
			str += skillControl.CoolingSkillFrame.ToString();
			str += "/";
			str += skillControl.NeedCoolFrame;

			return str;
		}
	}
}
