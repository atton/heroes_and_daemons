using UnityEngine;
using System.Collections;
using GameSystem;
using GameSystem.SettingDefinition;

namespace GameSystem.GameController {

	public class GameController : MonoBehaviour {

		// assign player character prefab on Unity GUI
		public GameObject SlayerPrefab;
		public GameObject GolemPrefab;


		/* network control identifier */
		protected NetworkPlayer SelfNetworkPlayer;

		/* GameController methods */

		protected virtual GameObject PlayerPrefabFromSetting() {
			GameObject prefab;

			switch (GlobalSettings.Setting.Character) {
				case Character.Slayer:
					prefab = SlayerPrefab;
					break;

				case Character.Golem:
					prefab = GolemPrefab;
					break;

				default:
					throw new UnityException("Prefab not found for " + GlobalSettings.Setting.Character.ToString());
			}

			return prefab;
		}

		protected virtual GameObject CharacterInstantiate(GameObject prefab, Vector3 position, Quaternion rotation) {
			GameObject obj;
			if (Network.peerType == NetworkPeerType.Server || Network.peerType == NetworkPeerType.Client) {
				obj = Network.Instantiate(prefab, position, rotation, 1) as GameObject;
			} else {
				obj = Instantiate(prefab, position, rotation) as GameObject;
			}
			return obj;
		}

		public virtual void NoticeKnockoutPlayer(NetworkPlayer pl) {
		}

	}

}

