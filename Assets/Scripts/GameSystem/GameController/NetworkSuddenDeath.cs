using UnityEngine;
using System.Collections;

namespace GameSystem {
	namespace GameController {

		public class NetworkSuddenDeath : GameController {

			Vector3 initPosition    = new Vector3(0.0f, 1.0f, 0.0f);
			Quaternion initRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

			void Awake() {
				CreateNewPlayerCharacter(SlayerPrefab);

			}

			void CreateNewPlayerCharacter(GameObject targetPrefab) {
				/* TODO: support others than slayer */
				GameObject obj = Network.Instantiate(targetPrefab, initPosition, initRotation, 1) as GameObject;
				int playerId = System.Int32.Parse(obj.networkView.owner.ToString());
				obj.transform.position += new Vector3(0.0f, 0.0f, playerId*3);
				if (obj.networkView.isMine) selfNetworkPlayer = obj.networkView.owner;
			}


			void OnPlayerDisconnected(NetworkPlayer pl) {
				Network.DestroyPlayerObjects(pl);
			}

			public override void NoticeKnockoutPlayer (NetworkPlayer pl) {
				if (selfNetworkPlayer == pl) {
					Debug.LogError("You Lose");
				} else {
					Debug.LogError("You Win");
				}
			}

		}

	}
}
