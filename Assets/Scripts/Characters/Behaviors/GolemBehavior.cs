using UnityEngine;
using System.Collections;
using StateMachine;
using CharacterInterface;
using GameSystem;
using GameSystem.SettingDefinition;
using GameSystem.GameController;

public class GolemBehavior : CharacterBehavior {

	public Rigidbody  golemShoot;
	public GameObject golemMelee;

	override protected void Awake () {
		parameter          = new GolemParameter();
		state              = new GolemStateMachine();
		characterAnimation = gameObject.GetComponent<GolemAnimation>();
		base.Awake();
	}

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

		case CharacterState.AttackStartMelee:
			AttackStartMeleeAction(frameCount);
			break;

		case CharacterState.AttackingMelee:
			AttackingMeleeAction(frameCount);
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

	/* Action Helpers */

	void move(Vector3 moveVector) {
		if (state.NowState() != CharacterState.Run) return;

		Vector3 runVector = parameter.RunSpeed * moveVector;

		rigidbody.AddForce(runVector);
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
		parameter.Damage(info);
		Debug.LogError("hit : id = " + networkView.owner.ToString() + ", HP = " + parameter.HitPoint);
		// TODO : show HP parameters

		if (parameter.SuperArmerDamageLimit <= info.DamageValue()) {
			// golem has super armer
			state.TryTransform(CharacterState.Hurt);
		}

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
		Vector3 spawnPoint = position + forward*5 + new Vector3(0, 2, 0);

		Rigidbody s         = Instantiate(golemShoot, spawnPoint, Quaternion.identity) as Rigidbody;
		s.velocity          = forward * 50;
		s.transform.forward = forward;
	}

	[RPC]
	void spawnMelee(Vector3 position, Vector3 forward) {
		Vector3 spawnPoint = position + forward + new Vector3(0, 2, 0);
		GameObject melee = Instantiate(golemMelee, spawnPoint, Quaternion.identity) as GameObject;

		melee.transform.forward = forward;
	}
}
