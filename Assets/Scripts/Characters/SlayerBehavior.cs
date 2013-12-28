using UnityEngine;
using System.Collections;
using StateMachine;
using CharacterInterface;

public class SlayerBehavior : CharacterBehavior {
	
	public Rigidbody  slayerShot;
	public GameObject slayerPunch;
	
	const float runSpeed = 20.0f;


	// Use this for initialization
	void Start () {
		state              = new SlayerStateMachine();
		characterAnimation = gameObject.GetComponent<SlayerAnimation>();
		enabled            = networkView.isMine;

		if (enabled) focusCamera();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateStateFromInput();
		state.UpdateFrameCount();
		ActionFromState();
		base.Update();
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
		if (frameCount == 0 || characterAnimation.IsFinishedNowAnimation()) {
			characterAnimation.PlayAnimationFromState(CharacterState.Stand);
		}

		if (transform.position.y > 0) state.TryTransform(CharacterState.Aerial);
	}

	void JumpStartAction(int frameCount) {
		if (frameCount == 0) characterAnimation.PlayAnimationFromState(CharacterState.JumpStart);
		if (characterAnimation.IsFinishedNowAnimation()) state.TryTransform(CharacterState.Jump);

	}
	void JumpAction(int frameCount) {
		if (frameCount == 0) {
			characterAnimation.PlayAnimationFromState(CharacterState.Jump);
			rigidbody.AddForce(new Vector3(0, 10 * runSpeed, 0));
		}
		if (characterAnimation.IsFinishedNowAnimation()) state.TryTransform(CharacterState.Aerial);
	}

	void AerialAction(int frameCount) {
		characterAnimation.PlayAnimationFromState(CharacterState.Aerial);

		if (transform.position.y <= 0) state.EndNowState();
	}

	void RunAction(int frameCount) {
		if (frameCount == 0 || characterAnimation.IsFinishedNowAnimation()) {
			characterAnimation.PlayAnimationFromState(CharacterState.Run);
		}

		if (rigidbody.velocity == Vector3.zero)	state.EndNowState();
	}

	void AttackStartMeleeAction(int frameCount) {
		if (frameCount == 0) characterAnimation.PlayAnimationFromState(CharacterState.AttackStartMelee);
		if (characterAnimation.IsFinishedNowAnimation()) state.TryTransform(CharacterState.AttackingMelee);
	}

	void AttackingMeleeAction(int frameCount) {
		if (frameCount == 0) {
			characterAnimation.PlayAnimationFromState(CharacterState.AttackingMelee);
			attackMelee();
		}
		if (characterAnimation.IsFinishedNowAnimation()) {
			state.EndNowState(); 
			// Please Add Skill cooling
		}
	}

	void AttackStartShootAction(int frameCount) {
		if (frameCount == 0) characterAnimation.PlayAnimationFromState(CharacterState.AttackStartShoot);

		if (characterAnimation.IsFinishedNowAnimation()) state.TryTransform(CharacterState.AttackingShoot);
	}

	void AttackingShootAction(int frameCount) {
		if (frameCount == 0) {
			characterAnimation.PlayAnimationFromState(CharacterState.AttackingShoot);
			shoot();
		}
		if (characterAnimation.IsFinishedNowAnimation()) {
			state.EndNowState(); 
			// Please Add Skill cooling
		}
	}
	
	void AttackRunShootAction(int frameCount) {
		if (frameCount == 0) {
			characterAnimation.PlayAnimationFromState(CharacterState.AttackRunShoot);
			shoot();
		}
		if (characterAnimation.IsFinishedNowAnimation()) {
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
		if (Network.peerType == NetworkPeerType.Client || Network.peerType == NetworkPeerType.Server) {
			networkView.RPC("spawnShoot", RPCMode.All, transform.position, transform.forward);
		} else {
			spawnShoot(transform.position, transform.forward);
		}
	}
	
	void attackMelee() {		
		if (Network.peerType == NetworkPeerType.Client || Network.peerType == NetworkPeerType.Server) {
			networkView.RPC("spawnMelee", RPCMode.All, transform.position, transform.forward);
		} else {
			spawnMelee(transform.position, transform.forward);
		}
	}

	/* interface methods */

	public override void Damage(DamageInfo info) {
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
	void spawnShoot(Vector3 position, Vector3 forward) {
		Vector3 spawnPoint = position + forward + new Vector3(0, 1, 0);
		
		Rigidbody shot         = Instantiate(slayerShot, spawnPoint, Quaternion.identity) as Rigidbody;
		shot.velocity          = forward * 10;
		shot.transform.forward = forward;
	}

	[RPC]
	void spawnMelee(Vector3 position, Vector3 forward) {
		Vector3 spawnPoint = position + forward;
		Instantiate(slayerPunch, spawnPoint, Quaternion.identity);
	}
}