using System;
using UnityEngine;

namespace GameSystem {
	public class PlayerController {
		/* definitions */
		private const KeyCode KeySkillA = KeyCode.Z;
		private const KeyCode KeySkillB = KeyCode.X;
		private const KeyCode KeySkillC = KeyCode.C;
		private const KeyCode KeyJump   = KeyCode.Space;

		/* cool down frames */

		private int neededCoolDownFrameSkillA;
		private int neededCoolDownFrameSkillB;
		private int neededCoolDownFrameSkillC;

		private int coolingFrameSkillA;
		private int coolingFrameSkillB;
		private int coolingFrameSkillC;


		public PlayerController () {
			neededCoolDownFrameSkillA = GlobalSettings.Setting.GetCoolDownFrameSkillA();
			neededCoolDownFrameSkillB = GlobalSettings.Setting.GetCoolDownFrameSkillB();
			neededCoolDownFrameSkillC = GlobalSettings.Setting.GetCoolDownFrameSkillC();

			coolingFrameSkillA = neededCoolDownFrameSkillA;
			coolingFrameSkillB = neededCoolDownFrameSkillB;
			coolingFrameSkillC = neededCoolDownFrameSkillC;
		}

		/* return CharacterControll from Inputs */
		public Vector3 GetMoveVectorFromInput() {
			float   upOrDown    = Input.GetAxis("Horizontal");
			float   rightOrLeft = Input.GetAxis("Vertical");
			Vector3 moveVector  = new Vector3(-rightOrLeft, 0, upOrDown);

			Debug.Log ("SkillA : " + coolingFrameSkillA.ToString() + " / " + neededCoolDownFrameSkillA.ToString());
			Debug.Log ("SkillB : " + coolingFrameSkillB.ToString() + " / " + neededCoolDownFrameSkillB.ToString());
			Debug.Log ("SkillB : " + coolingFrameSkillB.ToString() + " / " + neededCoolDownFrameSkillC.ToString());

			return moveVector;
		}

		public bool IsPressedJumpKey() {
			return Input.GetKeyUp(KeyJump);
		}

		public bool IsWantToUseSkillA() {
			return (neededCoolDownFrameSkillA <= coolingFrameSkillA++) && Input.GetKeyUp(KeySkillA);
		}
		
		public bool IsWantToUseSkillB() {
			return (neededCoolDownFrameSkillB <= coolingFrameSkillB++) && Input.GetKeyUp(KeySkillB);
		}
		
		public bool IsWantToUseSkillC() {
			return (neededCoolDownFrameSkillC <= coolingFrameSkillC++) && Input.GetKeyUp(KeySkillC);
		}

		public void UsedSkillA() {
			coolingFrameSkillA = 0;
		}

		public void UsedSkillB() {
			coolingFrameSkillB = 0;
		}
		
		public void UsedSkillC() {
			coolingFrameSkillC = 0;
		}
	}
}
