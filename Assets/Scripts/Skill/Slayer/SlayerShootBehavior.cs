using UnityEngine;
using System.Collections;
using CharacterInterface;

namespace Skill {
	namespace Slayer {

		public class SlayerShootBehavior : MonoBehaviour {

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

				DamageInfo info = new DamageInfo();
				info.SetDamageValue(shotDamageValue);

				MonoBehaviour[] behaviors =  collision.gameObject.GetComponents<MonoBehaviour>();
				foreach (MonoBehaviour b in behaviors) {
					IDamage i = b as IDamage;
					if (i != null) 	i.Damage(info);
				}

				Destroy(gameObject);
			}
		}

	}
}
