using UnityEngine;
using System.Collections;
using StateMachine;
using CharacterInterface;

public class CharacterBehavior : MonoBehaviour, IDamage {

	protected CharacterStateMachine state;
	protected CharacterAnimation    characterAnimation;

	/* TODO : this parameters split into character status class */
	public    int MaxHitPoint = 100;
	protected int hitPoint;


	/* TODO : this parameters split into field parameters class */
	protected float xRange = 3.0f;
	protected float zRange = 20.0f;


	protected virtual void Update() {
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

	public virtual void focusCamera() {
		GameObject mainCamera = GameObject.Find("Main Camera");
		Component[] components = mainCamera.GetComponents<MonoBehaviour>();
		
		foreach (Component c in components) {
			Cameracontrol control = c as Cameracontrol;
			if (control != null) control.player = gameObject;
		}
	}

}