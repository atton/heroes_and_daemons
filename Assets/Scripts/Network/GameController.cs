using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GameObject playerCharacter;	// assign player character prefab on GUI
	
	Vector3 initPosition    = new Vector3(0.0f, 1.0f, 0.0f);
	Quaternion initRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
	
	void Awake() {
		enabled = networkView.isMine;
		CreateNewPlayerCharacter();
	}

	void CreateNewPlayerCharacter() {
		Network.Instantiate(playerCharacter, initPosition, initRotation, 1);
	}
	
	void OnPlayerDisconnected(NetworkPlayer pl) {
		Network.DestroyPlayerObjects(pl);
	}
}
