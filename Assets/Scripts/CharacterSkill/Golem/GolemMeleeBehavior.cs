using UnityEngine;
using CharacterInterface;
using System.Collections;

namespace CharacterSkill.Golem {
	
	public class GolemMeleeBehavior : MonoBehaviour {
		
		public GameObject hitEffect;
		const float meleePower       = 100000.0f;
		const int   meleeDamageValue = 3000;
		
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			Destroy(gameObject);	// melee is 1 frame
		}
		
		void OnCollisionEnter(Collision collision) {
			Vector3 melee_vector = (collision.transform.position - transform.position);
			collision.gameObject.rigidbody.AddForce(meleePower * melee_vector);
			
			DamageInfo info = new DamageInfo();
			info.SetDamageValue(meleeDamageValue);
			
			MonoBehaviour[] behaviors =  collision.gameObject.GetComponents<MonoBehaviour>();
			foreach (MonoBehaviour b in behaviors) {
				IDamage i = b as IDamage;
				if (i != null) 	i.Damage(info);
			}
			
			Instantiate(hitEffect, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
	
}
