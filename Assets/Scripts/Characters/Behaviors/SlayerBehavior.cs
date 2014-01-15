using UnityEngine;
using System.Collections;
using StateMachine;
using CharacterInterface;
using GameSystem;
using GameSystem.SettingDefinition;
using GameSystem.GameController;

public class SlayerBehavior : CharacterBehavior {

	public Rigidbody  slayerShoot;
	public Rigidbody  slayerDash;
	public Rigidbody  slayerThrow;
	public GameObject slayerMelee;

	// Use this for initialization
	override protected void Awake () {
		parameter          = new SlayerParameter();
		state              = new SlayerStateMachine();
		characterAnimation = gameObject.GetComponent<SlayerAnimation>();
		base.Awake();
	}

	// Update is called once per frame
	override protected void Update () {
		playerController.UpdateCharacterFromInput(this);
		state.UpdateFrameCount();
		ActionFromState();
		base.Update();
	}

	/* IControllable methods */
	public override void Move(Vector3 moveVector) {
		if (moveVector == Vector3.zero) return;
		state.TryTransform(CharacterState.Run);
		move(moveVector);

	}

	public override void Jump() {
		state.TryTransform(CharacterState.JumpStart);
	}

	public override bool UseSkill(Skill s) {
		return TryTransfromFromSkill(s);
	}

	/* Actions */

	void ActionFromState() {
		CharacterState cs = state.NowState();
		int frameCount    = state.FrameCount();

		if (frameCount == 0) characterAnimation.PlayAnimationFromState(cs);

		switch (cs) {

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

		case CharacterState.Hurt:
			HurtAction(frameCount);
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
		
		case CharacterState.AttackStartDash:
			AttackStartDashAction(frameCount);
			break;

		case CharacterState.AttackingDash:
			AttackingDashAction(frameCount);
			break;

		case CharacterState.AttackStartThrow:
			AttackStartThrowAction(frameCount);
			break;

		case CharacterState.AttackingThrow:
			AttackingThrowAction(frameCount);
			break;

		default:
			throw new UnityException("unknown state : " + state.NowState());
		}
	}

	/* Action Details */

	void StandAction(int frameCount) {
		if (transform.position.y > 0) state.TryTransform(CharacterState.Aerial);
	}

	void JumpStartAction(int frameCount) {
		if (characterAnimation.IsFinishedNowAnimation()) state.TryTransform(CharacterState.Jump);
	}

	void JumpAction(int frameCount) {
		if (frameCount == 0) {
			rigidbody.AddForce(new Vector3(0, 10 * parameter.RunSpeed, 0));
		}
		if (characterAnimation.IsFinishedNowAnimation()) state.TryTransform(CharacterState.Aerial);
	}

	void AerialAction(int frameCount) {
		if (transform.position.y <= 0) state.EndNowState();
	}

	void HurtAction(int frameCount) {
		if (characterAnimation.IsFinishedNowAnimation()) state.EndNowState();
	}

	void RunAction(int frameCount) {
		if (rigidbody.velocity == Vector3.zero)	state.EndNowState();
	}

	void AttackStartMeleeAction(int frameCount) {
		if (characterAnimation.IsFinishedNowAnimation()) state.TryTransform(CharacterState.AttackingMelee);
	}

	void AttackingMeleeAction(int frameCount) {
		if (frameCount == 0) {
			attackMelee();
		}
		if (characterAnimation.IsFinishedNowAnimation()) {
			state.EndNowState();
		}
	}

	void AttackStartShootAction(int frameCount) {
		if (frameCount == 0) characterAnimation.PlayAnimationFromState(CharacterState.AttackStartShoot);

		if (characterAnimation.IsFinishedNowAnimation()) state.TryTransform(CharacterState.AttackingShoot);
	}

	void AttackingShootAction(int frameCount) {
		if (frameCount == 0) {
			shoot();
		}
		if (characterAnimation.IsFinishedNowAnimation()) {
			state.EndNowState();
		}
	}

	void AttackRunShootAction(int frameCount) {
		if (frameCount == 0) {
			shoot();
		}
		if (characterAnimation.IsFinishedNowAnimation()) {
			state.EndNowState();
		}
	}

	void AttackStartDashAction(int frameCount) {
		if (characterAnimation.IsFinishedNowAnimation()) state.TryTransform(CharacterState.AttackingDash);
	}

	void AttackingDashAction(int frameCount) {
		if (frameCount == 0) {
			dash();
		}

		if (60 <= frameCount) {
			state.EndNowState(); 
		}
	}

	void AttackStartThrowAction(int frameCount) {
		if (characterAnimation.IsFinishedNowAnimation()) state.TryTransform(CharacterState.AttackingThrow);
	}
	
	void AttackingThrowAction(int frameCount) {
		if (frameCount == 0) {
			throwmagic();
		}
		if (characterAnimation.IsFinishedNowAnimation()) {
			state.EndNowState(); 
		}
	}

	/* Action Helpers */

	void move(Vector3 moveVector) {
		CharacterState s = state.NowState();
		if (!(s == CharacterState.Run || s == CharacterState.Aerial)) return;

		Vector3 runVector = parameter.RunSpeed * moveVector;

		rigidbody.AddForce(runVector);
	 	transform.rotation = Quaternion.LookRotation(moveVector);
	}
	
	void dash() {
		if (Network.peerType == NetworkPeerType.Client || Network.peerType == NetworkPeerType.Server) {
			networkView.RPC("spawnDash", RPCMode.All, transform.position, transform.forward);
		} else {
			spawnDash(transform.position, transform.forward);
		}
	}

	void shoot() {
		if (Network.peerType == NetworkPeerType.Client || Network.peerType == NetworkPeerType.Server) {
			networkView.RPC("spawnShoot", RPCMode.All, transform.position, transform.forward);
		} else {
			spawnShoot(transform.position, transform.forward);
		}
	}

	void throwmagic() {
		if (Network.peerType == NetworkPeerType.Client || Network.peerType == NetworkPeerType.Server) {
			networkView.RPC("spawnThrow", RPCMode.All, transform.position, transform.forward);
		} else {
			spawnThrow(transform.position, transform.forward);
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
		parameter.Damage(info);
		state.TryTransform(CharacterState.Hurt);	// slayer not has super armer

		if (parameter.HitPoint <= 0) {
			gameController.NoticeKnockoutPlayer(networkView.owner);
		}
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

		Rigidbody shot         = Instantiate(slayerShoot, spawnPoint, Quaternion.identity) as Rigidbody;
		shot.velocity          = forward * 10;
		shot.transform.forward = forward;
	}

	[RPC]
	void spawnThrow(Vector3 position, Vector3 forward) {
		Vector3 spawnPoint     = position + forward + new Vector3(0, 1, 0);
		Rigidbody throwMagic   = Instantiate(slayerThrow, spawnPoint, Quaternion.identity) as Rigidbody;
		throwMagic.velocity    = forward * 10 + new Vector3(0, 10, 0);
		throwMagic.transform.forward = forward;
		throwMagic.transform.position += forward;
	}

	[RPC]
	void spawnDash(Vector3 position, Vector3 forward) {
		Vector3 spawnPoint   = position + forward  + new Vector3(0, 1, 0);
		Rigidbody dashEffect = Instantiate(slayerDash, spawnPoint, Quaternion.identity) as Rigidbody;
		dashEffect.transform.forward = forward;
		dashEffect.transform.position += forward;
		Vector3  force = forward * 3000;
		dashEffect.rigidbody.AddForce(force);
		rigidbody.AddForce(force);
	}

	[RPC]
	void spawnMelee(Vector3 position, Vector3 forward) {
		Vector3 spawnPoint = position + forward + new Vector3(0, 1, 0);
		Instantiate(slayerMelee, spawnPoint, Quaternion.identity);
	}
}
