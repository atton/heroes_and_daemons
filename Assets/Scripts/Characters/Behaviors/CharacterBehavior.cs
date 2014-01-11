using UnityEngine;
using System.Collections;
using GameSystem.GameController;
using StateMachine;
using CharacterInterface;

public class CharacterBehavior : MonoBehaviour, IDamage {

	protected GameController controller;

	protected CharacterStateMachine state;
	protected CharacterAnimation    characterAnimation;

	protected CharacterParameter    parameter;

	/* TODO : this parameters split into field parameters class */
	protected float xRange = 3.0f;
	protected float zRange = 20.0f;

	protected virtual void Awake() {
		controller         = Object.FindObjectOfType<GameController>();
		enabled            = networkView.isMine;
		if (enabled) focusCamera();
	}


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
		parameter.Damage(info);
	}

	public virtual void focusCamera() {
		GameObject mainCamera = GameObject.Find("Main Camera");
		Component[] components = mainCamera.GetComponents<MonoBehaviour>();
		
		foreach (Component c in components) {
			Cameracontrol control = c as Cameracontrol;
			if (control != null) control.player = gameObject;
		}
	}

	/* getter setter */
	public GameController Controller {
		get { return this.controller; }
		set { this.controller = value; }
	}
}
