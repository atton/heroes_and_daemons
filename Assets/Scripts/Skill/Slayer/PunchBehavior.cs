using UnityEngine;
using CharacterInterface;
using System.Collections;

namespace Skill {
	namespace Slayer { 

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

				DamageInfo info = new DamageInfo();
				info.SetDamageValue(punchDamageValue);

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
}
