using UnityEngine;
using System.Collections;
using GameSystem;
using GameSystem.SettingDefinition;

namespace GameSystem {
	namespace GameController {

		public class GameController : MonoBehaviour {

			// assign player character prefab on Unity GUI
			public GameObject SlayerPrefab;
			public GameObject GolemPrefab;

			protected NetworkPlayer selfNetworkPlayer;

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

			public virtual void NoticeKnockoutPlayer(NetworkPlayer pl) {
			}

		}

	}
}
