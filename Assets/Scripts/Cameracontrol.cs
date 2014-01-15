using UnityEngine;
using System.Collections;

public class Cameracontrol : MonoBehaviour {
	public GameObject player;
	public float starterhigh;
	// Use this for initialization
	void Start () {
		//player=GameObject.Find("Slayer");
		starterhigh=transform.position.y;
	}

	// Update is called once per frame
	void Update () {

		transform.position=new Vector3(transform.position.x,
		                               player.transform.position.y+this.starterhigh,
		                               player.transform.position.z
		                               );
	}
}
