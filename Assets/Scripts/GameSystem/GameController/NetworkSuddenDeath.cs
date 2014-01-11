using UnityEngine;
using System.Collections;

namespace GameSystem.GameController {

	public class NetworkSuddenDeath : GameController {

		Vector3 initPosition    = new Vector3(0.0f, 1.0f, 0.0f);
		Quaternion initRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

		void Awake() {
			CreateNewPlayerCharacter(PlayerPrefabFromSetting());

		}

		void CreateNewPlayerCharacter(GameObject targetPrefab) {
			GameObject obj = CharacterInstantiate(targetPrefab, initPosition, initRotation);
			int playerId = System.Int32.Parse(obj.networkView.owner.ToString());
			obj.transform.position += new Vector3(0.0f, 0.0f, playerId*3);
			if (obj.networkView.isMine) SelfNetworkPlayer = obj.networkView.owner;
		}


		void OnPlayerDisconnected(NetworkPlayer pl) {
			Network.DestroyPlayerObjects(pl);
		}

		public override void NoticeKnockoutPlayer(NetworkPlayer pl) {
			/* TODO: show win/lose result */
			if (SelfNetworkPlayer == pl) {
				Debug.LogError("You Lose");
			} else {
				Debug.LogError("You Win");
			}
		}

	}

}
