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
			GameObject obj = Instantiate(spawnPrefab, spawnPoint, spawnRotation) as GameObject;
			CharacterBehavior character = obj.GetComponent<CharacterBehavior>();
			character.Controller = this;
		}

		// Update is called once per frame
		void Update () {

		}

		public override void NoticeKnockoutPlayer (NetworkPlayer pl) {
			Debug.Log("D Mascot is deleted.");
		}

	}

}
