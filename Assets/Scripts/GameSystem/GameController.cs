using UnityEngine;
using System.Collections;

namespace GameSystem {

	public class GameController : MonoBehaviour {

		// assign player character prefab on Unity GUI
		public GameObject SlayerPrefab;

		protected NetworkPlayer selfNetworkPlayer;

		public virtual void NoticeKnockoutPlayer(NetworkPlayer pl) {
		}
	}

}