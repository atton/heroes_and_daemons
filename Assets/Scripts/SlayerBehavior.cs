using UnityEngine;
using System.Collections;
using StateMachine;
using CharacterInterface;

public class SlayerBehavior : MonoBehaviour, IDamage {
	
	public Rigidbody  slayerShot;
	public GameObject slayerPunch;
	
	const float runSpeed = 20.0f;
	private int hitPoint = 100; // temporary set variable. TODO: create character parameter class;

	private SlayerStateMachine state;

	void Awake() {
		enabled = networkView.isMine;
	}

	// Use this for initialization
	void Start () {
		state = new SlayerStateMachine();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateStateFromInput();
		state.UpdateFrameCount();
		ActionFromState();
	}

	void UpdateStateFromInput() {
		/* TODO: split this function into character controller class */
		
		// input from cursor keys
		float   up_or_down    = Input.GetAxis("Horizontal");
		float   right_or_left = Input.GetAxis("Vertical");
		Vector3 moveVector    = new Vector3(-right_or_left, 0, up_or_down);

		// Run Action
		if ((moveVector != Vector3.zero)) {
			state.TryTransform(CharacterState.Run);
			move(moveVector);
		}

		if (Input.GetKeyUp(KeyCode.Space)) {
			state.TryTransform(CharacterState.JumpStart);
		}

		// Shot Action
		if (Input.GetKeyUp(KeyCode.X)) {
			state.TryTransform(CharacterState.AttackStartShoot);
			state.TryTransform(CharacterState.AttackRunShoot);
		}

		// Punch Action
		if (Input.GetKeyUp(KeyCode.Z)) {
			state.TryTransform(CharacterState.AttackStartMelee);
		}
		
	}

	void ActionFromState() {
		int frameCount = state.FrameCount();

		switch (state.NowState()) {

		case CharacterState.Stand:
			StandAction(frameCount);
			break;
			
		case CharacterState.Run:
			RunAction(frameCount);
			break;

		case CharacterState.JumpStart:
			JumpStartAction(frameCount);
			break;

		case CharacterState.Jump:
			JumpAction(frameCount);
			break;

		case CharacterState.Aerial:
			AerialAction(frameCount);
			break;

		case CharacterState.AttackStartShoot:
			AttackStartShootAction(frameCount);
			break;
		
		case CharacterState.AttackingShoot:
			AttackingShootAction(frameCount);
			break;
		
		case CharacterState.AttackRunShoot:
			AttackRunShootAction(frameCount);
			break;

		case CharacterState.AttackStartMelee:
			AttackStartMeleeAction(frameCount);
			break;

		case CharacterState.AttackingMelee:
			AttackingMeleeAction(frameCount);
			break;
		
		default:
			Debug.Log("unknown state : " + state.NowState());
			break;
		}
	}

	/* Actions */

	void StandAction(int frameCount) {
		animation.Play("stand");

		if (transform.position.y > 0) state.TryTransform(CharacterState.Aerial);
	}

	void JumpStartAction(int frameCount) {
		if (frameCount == 0) animation.Play("jumpstart");
		if (!animation.IsPlaying("jumpstart")) state.TryTransform(CharacterState.Jump);

	}
	void JumpAction(int frameCount) {
		if (frameCount == 0) {
			animation.Play("jump");
			rigidbody.AddForce(new Vector3(0, 10 * runSpeed, 0));
		}
		if (!animation.IsPlaying("jump")) state.TryTransform(CharacterState.Aerial);
	}

	void AerialAction(int frameCount) {
		animation.Play("jump");
		animation["jump"].time = animation["jump"].length;      // last frame of jump animation

		if (transform.position.y <= 0) state.EndNowState();
	}

	void RunAction(int frameCount) {
		animation.Play("run");

		if (rigidbody.velocity == Vector3.zero) {
			state.EndNowState();
		}
	}

	void AttackStartMeleeAction(int frameCount) {
		if (frameCount == 0) animation.Play("attackstartmelee");
		if (!animation.IsPlaying("attackstartmelee")) state.TryTransform(CharacterState.AttackingMelee);
	}

	void AttackingMeleeAction(int frameCount) {
		if (frameCount == 0) {
			animation.Play("attackingmelee");
			punch();
		}
		if (!animation.IsPlaying("attackingmelee")) {
			state.EndNowState(); 
			// Please Add Skill cooling
		}
	}

	void AttackStartShootAction(int frameCount) {
		if (frameCount == 0) animation.Play("attackstartshoot");

		if (!animation.IsPlaying("attackstartshoot")) state.TryTransform(CharacterState.AttackingShoot);
	}

	void AttackingShootAction(int frameCount) {
		if (frameCount == 0) {
			animation.Play("attackingshoot");
			shoot();
		}
		if (!animation.IsPlaying("attackingshoot")) {
			state.EndNowState(); 
			// Please Add Skill cooling
		}
	}
	
	void AttackRunShootAction(int frameCount) {
		if (frameCount == 0) {
			animation.Play("attackrunshoot");
			shoot();
		}
		if (!animation.IsPlaying("attackrunshoot")) {
			state.EndNowState(); 
			// Please Add Skill cooling
		}
	}

	/* Action Helpers */
	
	void move(Vector3 moveVector) {
		if (state.NowState() != CharacterState.Run) return;

		Vector3 run_vector = runSpeed * moveVector;
	
		rigidbody.AddForce(run_vector);
	 	transform.rotation = Quaternion.LookRotation(moveVector);
	}
	
	void shoot() {
		networkView.RPC("netShoot", RPCMode.All, transform.position, transform.forward);
	}
	
	void punch() {		
		Vector3 spawnPoint = transform.position + transform.forward;
		Instantiate(slayerPunch, spawnPoint, Quaternion.identity);
	}

	/* interface methods */

	public void Damage(DamageInfo info) {
		hitPoint -= info.DamageValue();

		Debug.Log(hitPoint);
	}
	
	/* utils */
	
	string GetCurrentAnimationName() {
		foreach (AnimationState anim in animation) {
			if (animation.IsPlaying(anim.name)) return anim.name;
		}
		return "AnimationNotFound";
	}

	/* RPCs */
	[RPC]
	void netShoot(Vector3 position, Vector3 forward) {
		Vector3 spawnPoint = position + forward + new Vector3(0, 1, 0);
		
		Rigidbody shot         = Instantiate(slayerShot, spawnPoint, Quaternion.identity) as Rigidbody;
		shot.velocity          = forward * 10;
		shot.transform.forward = forward;
	}
}