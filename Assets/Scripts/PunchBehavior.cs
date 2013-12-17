using UnityEngine;
using CharacterInterface;
using System.Collections;

public class PunchBehavior : MonoBehaviour {
	
	public GameObject hitEffect;
	const float punchPower       = 10000.0f;
	const int   punchDamageValue = 50;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Destroy(gameObject);	// punch is 1 frame
	}
	
	void OnCollisionEnter(Collision collision) {
		Vector3 punch_vector = (collision.transform.position - transform.position);
		collision.gameObject.rigidbody.AddForce(punchPower * punch_vector);

		
		// FIXME: check IDamage interface and call Damage method in this place. but it's send message style.
		DamageInfo info = new DamageInfo();
		info.SetDamageValue(punchDamageValue);
		collision.gameObject.SendMessage("Damage", info, SendMessageOptions.DontRequireReceiver);
		
		Instantiate(hitEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
