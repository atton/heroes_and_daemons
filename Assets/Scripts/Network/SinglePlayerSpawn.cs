using UnityEngine;
using System.Collections;

public class SinglePlayerSpawn : MonoBehaviour {

	public GameObject spawnPrefab;
	Vector3    spawnPoint    = new Vector3(0, 1, 0);
	Quaternion spawnRotation = new Quaternion(0, 0, 0, 0);

	// Use this for initialization
	void Start () {
		GameObject player = Instantiate(spawnPrefab, spawnPoint, spawnRotation) as GameObject;
		GameObject mainCamera = GameObject.Find("Main Camera");
		Component[] components = mainCamera.GetComponents<MonoBehaviour>();

		foreach (Component c in components) {
			Cameracontrol control = c as Cameracontrol;
			if (control != null) control.player = player;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
