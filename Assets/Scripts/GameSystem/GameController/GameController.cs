using UnityEngine;
using System.Collections;

namespace GameSystem {
	namespace GameController {

		public class GameController : MonoBehaviour {

			// assign player character prefab on Unity GUI
			public GameObject SlayerPrefab;

			protected NetworkPlayer selfNetworkPlayer;

			public virtual void NoticeKnockoutPlayer(NetworkPlayer pl) {
			}
		}

	}
}
