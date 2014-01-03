﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GameObject playerCharacter;	// assign player character prefab on GUI
	
	Vector3 initPosition    = new Vector3(0.0f, 1.0f, 0.0f);
	Quaternion initRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
	
	void Awake() {
		CreateNewPlayerCharacter();
	}

	void CreateNewPlayerCharacter() {
		GameObject obj = Network.Instantiate(playerCharacter, initPosition, initRotation, 1) as GameObject;
		int playerId = System.Int32.Parse(obj.networkView.owner.ToString());
		obj.transform.position += new Vector3(0.0f, 0.0f, playerId*3);
	}
	
	void OnPlayerDisconnected(NetworkPlayer pl) {
		Network.DestroyPlayerObjects(pl);
	}
}
