using UnityEngine;
using System.Collections;
using StateMachine;
using CharacterInterface;

public class CharacterBehavior : MonoBehaviour, IDamage {

	protected CharacterStateMachine state;

	public    int MaxHitPoint = 100;
	protected int hitPoint;
	
	public virtual void Damage(DamageInfo info) {
		hitPoint -= info.DamageValue();
		
		if (hitPoint <= 0) {
			// TODO: Add mini effect?
			Destroy(gameObject);
		}
	}

}