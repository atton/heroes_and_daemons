using UnityEngine;
using System.Collections;
using StateMachine;
using CharacterInterface;
using GameSystem;
using GameSystem.GameController;

public class SlayerBehavior : CharacterBehavior {
	
	public Rigidbody  slayerShoot;
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
		UpdateStateFromInput();
		state.UpdateFrameCount();
		ActionFromState();
		base.Update();
	}

	void UpdateStateFromInput() {
		Vector3 moveVector = playerController.GetMoveVectorFromInput();

		// move. support only Run state
		if ((moveVector != Vector3.zero)) {
			state.TryTransform(CharacterState.Run);
			move(moveVector);
		}

		// jump
		if (playerController.IsPressedJumpKey()) {
			state.TryTransform(CharacterState.JumpStart);
		}

		// Skills
		if (playerController.IsWantToUseSkillA()) {
			bool successedTransform = TryTransfromFromSkill(GlobalSettings.Setting.SkillA);
			if (successedTransform) playerController.UsedSkillA();
		}
		if (playerController.IsWantToUseSkillB()) {
			bool successedTransfrom = TryTransfromFromSkill(GlobalSettings.Setting.SkillB);
			if (successedTransfrom) playerController.UsedSkillB();
		}
		if (playerController.IsWantToUseSkillC()){
			bool successedTransfrom = TryTransfromFromSkill(GlobalSettings.Setting.SkillC);
			if (successedTransfrom) playerController.UsedSkillC();
		}
	}

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
		
		default:
			throw new UnityException("unknown state : " + state.NowState());
		}
	}

	/* Actions */

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
			// Please Add Skill cooling
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
			// Please Add Skill cooling
		}
	}
	
	void AttackRunShootAction(int frameCount) {
		if (frameCount == 0) {
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

		// TODO : check hurt condition if required. this implement is force hurt in Damage.
		state.TryTransform(CharacterState.Hurt);

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
	void spawnMelee(Vector3 position, Vector3 forward) {
		Vector3 spawnPoint = position + forward;
		Instantiate(slayerMelee, spawnPoint, Quaternion.identity);
	}
}
