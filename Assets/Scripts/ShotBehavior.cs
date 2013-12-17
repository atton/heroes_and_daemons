using UnityEngine;
using System.Collections;
using CharacterInterface;

public class ShotBehavior : MonoBehaviour {
	
	public GameObject hitEffect;
	public int        shotDurationFrame = 120;
	public int        shotDamageValue   = 10;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		shotDurationFrame--;
		if (shotDurationFrame == 0) Destroy(gameObject);
	
	}
	
	void OnCollisionEnter(Collision collision) {
		Instantiate(hitEffect, transform.position, Quaternion.identity);

		// FIXME: check IDamage interface and call Damage method in this place. but it's send message style.
		DamageInfo info = new DamageInfo();
		info.SetDamageValue(shotDamageValue);
		collision.gameObject.SendMessage("Damage", info, SendMessageOptions.DontRequireReceiver);

		Destroy(gameObject);
	}
}
