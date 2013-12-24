using UnityEngine;
using System.Collections;
using StateMachine;
using CharacterInterface;

public class CharacterBehavior : MonoBehaviour, IDamage {

	protected CharacterStateMachine state;

	/* TODO : this parameters split into character status class */
	public    int MaxHitPoint = 100;
	protected int hitPoint;


	/* TODO : this parameters split into field parameters class */
	protected float xRange = 3.0f;
	protected float zRange = 20.0f;


	protected void Update() {
		positionControl();
	}

	void positionControl() {
		Vector3 pos = transform.position;

		pos.x = Mathf.Clamp(pos.x, -xRange, xRange);
		pos.z = Mathf.Clamp(pos.z, -zRange, zRange);
		
		transform.position = pos;
	}
	
	public virtual void Damage(DamageInfo info) {
		hitPoint -= info.DamageValue();
		
		if (hitPoint <= 0) {
			// TODO: Add mini effect?
			Destroy(gameObject);
		}
	}

}