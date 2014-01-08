using System;

namespace StateMachine {
	public enum CharacterState {
		None = 0,
		Stand,
		Aerial,
		Hurt,
		JumpStart,
		Jump,
		Run,
		AttackingBarrier,
		AttackingBurst,
		AttackingDash,
		AttackingMelee,
		AttackingShoot,
		AttackingThrow,
		AttackRunShoot,
		AttackStartBarrier,
		AttackStartBurst,
		AttackStartDash,
		AttackStartMelee,
		AttackStartShoot,
		AttackStartThrow
	}
}
