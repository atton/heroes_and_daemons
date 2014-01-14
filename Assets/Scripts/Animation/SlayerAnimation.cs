using UnityEngine;
using System;
using StateMachine;

public class SlayerAnimation : CharacterAnimation {

	public void Awake() {
		animation["stand"].wrapMode = WrapMode.Loop;
		animation["run"].wrapMode   = WrapMode.Loop;
	}

	public override void PlayAnimationFromState (StateMachine.CharacterState cs) {

		switch (cs) {

		case CharacterState.Stand:
			PlayAnimation("stand");
			break;

		case CharacterState.JumpStart:
			PlayAnimation("jumpstart");
			break;

		case CharacterState.Jump:
			PlayAnimation("jump");
			break;

		case CharacterState.Aerial:
			PlayAnimation("jump");
			animation["jump"].time = animation["jump"].length;      // last frame of jump animation
			break;
		
		case CharacterState.Hurt:
			PlayAnimation("hurt");
			break;
		
		case CharacterState.Run:
			PlayAnimation("run");
			break;
		
		case CharacterState.AttackStartMelee:
			PlayAnimation("attackstartmelee");
			break;
		
		case CharacterState.AttackingMelee:
			PlayAnimation("attackingmelee");
			break;
		
		case CharacterState.AttackStartShoot:
			PlayAnimation("attackstartshoot");
			break;
		
		case CharacterState.AttackingShoot:
			PlayAnimation("attackingshoot");
			break;
		
		case CharacterState.AttackRunShoot:
			PlayAnimation("attackrunshoot");
			break;
		
		case CharacterState.AttackStartDash:
			PlayAnimation("attackstartdash");
			break;

		case CharacterState.AttackingDash:
			PlayAnimation("attackingdash");
			break;

		case CharacterState.AttackStartThrow:
			PlayAnimation("attackstartthrow");
			break;
		case CharacterState.AttackingThrow:
			PlayAnimation("attackingthrow");
			break;
		default:
			throw new UnityException("Undefined actions to " + cs.ToString());
		}

		base.PlayAnimationFromState(cs);
	}
	
}
