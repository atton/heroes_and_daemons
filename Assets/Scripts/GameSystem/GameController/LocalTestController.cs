using UnityEngine;
using System.Collections;

// spawn single player for local test environment

namespace GameSystem.GameController {

	public class LocalTestController : GameController {

		public GameObject spawnPrefab;
		Vector3    spawnPoint    = new Vector3(0, 1, 0);
		Quaternion spawnRotation = new Quaternion(0, 0, 0, 0);

		// Use this for initialization
		void Start () {
			CharacterInstantiate(spawnPrefab, spawnPoint, spawnRotation);
		}

		public override void NoticeKnockoutPlayer (NetworkPlayer pl) {
			Debug.Log("D Mascot is deleted.");
		}

	}

}
