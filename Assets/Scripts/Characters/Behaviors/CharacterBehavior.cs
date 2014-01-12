using UnityEngine;
using System.Collections;
using GameSystem;
using GameSystem.GameController;
using GameSystem.SettingDefinition;
using StateMachine;
using CharacterInterface;

public class CharacterBehavior : MonoBehaviour, IDamage, IControllable {

	protected GameController   gameController;
	protected PlayerController playerController;

	protected CharacterStateMachine state;
	protected CharacterAnimation    characterAnimation;

	protected CharacterParameter    parameter;

	/* TODO : this parameters split into field parameters class */
	protected float xRange = 3.0f;
	protected float zRange = 20.0f;

	protected virtual void Awake() {
		gameController     = Object.FindObjectOfType<GameController>();
		playerController   = new PlayerController();
		enabled            = networkView.isMine;
		if (enabled) FocusCamera();
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

	/* public methods */	

	public virtual void FocusCamera() {
		GameObject mainCamera = GameObject.Find("Main Camera");
		Component[] components = mainCamera.GetComponents<MonoBehaviour>();
		
		foreach (Component c in components) {
			Cameracontrol control = c as Cameracontrol;
			if (control != null) control.player = gameObject;
		}
	}

	/* IControllable methods */

	public virtual void Move(Vector3 moveVector) {
		throw new UnityEngine.UnityException("UnImplemented Move method. please override it.");
	}

	public virtual void Jump() {
		throw new UnityEngine.UnityException("UnImplemented Jump method. please override it.");
	}

	public virtual bool UseSkill(Skill s) {
		throw new UnityEngine.UnityException("UnImplemented UseSkill method. please override it.");
	}

	/* utils for inherited class */
	
	protected bool TryTransfromFromSkill(Skill s) {
		bool successedTransfrom = false;

		switch (s) {
		case Skill.Melee:
			successedTransfrom |= state.TryTransform(CharacterState.AttackStartMelee);
			break;
		case Skill.Shoot:
			successedTransfrom |= state.TryTransform(CharacterState.AttackStartShoot);
			successedTransfrom |= state.TryTransform(CharacterState.AttackRunShoot);
			break;

		default:
			throw new UnityException("Undefined Transform for skill : " + s.ToString());
		}

		return successedTransfrom;
	}

	/* getter setter */
	public GameController Controller {
		get { return this.gameController; }
		set { this.gameController = value; }
	}
}
