﻿using UnityEngine;
using System.Collections;

// spawn single player for local test environment

namespace GameSystem {

	public class LocalTestController : GameController {

		public GameObject spawnPrefab;
		Vector3    spawnPoint    = new Vector3(0, 1, 0);
		Quaternion spawnRotation = new Quaternion(0, 0, 0, 0);

		// Use this for initialization
		void Start () {
			Instantiate(spawnPrefab, spawnPoint, spawnRotation);
		}

		// Update is called once per frame
		void Update () {

		}
	}

}
