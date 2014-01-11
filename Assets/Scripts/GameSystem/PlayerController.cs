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

		private int coolDownFrameSkillA;
		private int coolDownFrameSkillB;
		private int coolDownFrameSkillC;

		public PlayerController () {
		}

		/* return CharacterControll from Inputs */
		public Vector3 GetMoveVectorFromInput() {
			float   upOrDown    = Input.GetAxis("Horizontal");
			float   rightOrLeft = Input.GetAxis("Vertical");
			Vector3 moveVector  = new Vector3(-rightOrLeft, 0, upOrDown);
			return moveVector;
		}

		public bool IsPressedJumpKey() {
			return Input.GetKeyUp(KeyJump);
		}

		/* TODO: calculate cooldown for skills */

		public bool IsWantToUseSkillA() {
			return Input.GetKeyUp(KeySkillA);
		}
		
		public bool IsWantToUseSkillB() {
			return Input.GetKeyUp(KeySkillB);
		}
		
		public bool IsWantToUseSkillC() {
			return Input.GetKeyUp(KeySkillC);
		}
	}
}
