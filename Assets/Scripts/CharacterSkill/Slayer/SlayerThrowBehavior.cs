using UnityEngine;
using System.Collections;
using CharacterInterface;

namespace CharacterSkill.Slayer {
	
	public class SlayerThrowBehavior : MonoBehaviour {
		
		public GameObject hitEffect;
		public int        throwDurationFrame = 120;
		public int        throwDamageValue   = 2500;
		
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			throwDurationFrame--;
			if (throwDurationFrame == 0) Destroy(gameObject);
			
		}
		
		void OnCollisionEnter(Collision collision) {
			Instantiate(hitEffect, transform.position, Quaternion.identity);
			
			DamageInfo info = new DamageInfo();
			info.SetDamageValue(throwDamageValue);
			
			MonoBehaviour[] behaviors =  collision.gameObject.GetComponents<MonoBehaviour>();
			foreach (MonoBehaviour b in behaviors) {
				IDamage i = b as IDamage;
				if (i != null) 	i.Damage(info);
			}
			
			Destroy(gameObject);
		}
	}
	
}
