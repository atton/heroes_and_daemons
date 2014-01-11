using System;
using UnityEngine;

namespace GameSystem {
	public class PlayerController {
		public PlayerController () {
		}

		public void Update(CharacterBehavior character) {
			if (Input.GetKeyUp(KeyCode.Space)) {
				Debug.Log ("detect space key");
			}

		}
	}
}
