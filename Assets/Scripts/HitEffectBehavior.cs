using UnityEngine;
using System.Collections;

public class HitEffectBehavior : MonoBehaviour {

	public int remainFrame = 5;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (remainFrame-- == 0) Destroy(gameObject);
	}
}
