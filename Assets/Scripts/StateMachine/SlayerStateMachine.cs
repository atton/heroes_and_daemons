using System;
namespace StateMachine
{
	public class SlayerStateMachine : CharacterStateMachine	{
		public SlayerStateMachine() : base(CharacterState.Stand) {
			SetTransformCondition(CharacterState.Stand, CharacterState.Run);
			SetTransformCondition(CharacterState.Stand, CharacterState.Hurt);
			SetTransformCondition(CharacterState.Stand, CharacterState.JumpStart);
			SetTransformCondition(CharacterState.Stand, CharacterState.Aerial);
			SetTransformCondition(CharacterState.Stand, CharacterState.AttackStartBarrier);
			SetTransformCondition(CharacterState.Stand, CharacterState.AttackStartBurst);
			SetTransformCondition(CharacterState.Stand, CharacterState.AttackStartDash);
			SetTransformCondition(CharacterState.Stand, CharacterState.AttackStartMelee);
			SetTransformCondition(CharacterState.Stand, CharacterState.AttackStartShoot);
			SetTransformCondition(CharacterState.Stand, CharacterState.AttackStartThrow);

			SetTransformCondition(CharacterState.Run, CharacterState.AttackRunShoot);
			SetTransformCondition(CharacterState.Run, CharacterState.AttackStartMelee);
			SetTransformCondition(CharacterState.Run, CharacterState.Hurt);

			SetTransformCondition(CharacterState.JumpStart, CharacterState.Jump);
			SetTransformCondition(CharacterState.JumpStart, CharacterState.Hurt);

			SetTransformCondition(CharacterState.Jump, CharacterState.Aerial);
			SetTransformCondition(CharacterState.Jump, CharacterState.Hurt);

			SetTransformCondition(CharacterState.AttackStartBarrier, CharacterState.AttackingBarrier);
			SetTransformCondition(CharacterState.AttackStartBarrier, CharacterState.Hurt);

			SetTransformCondition(CharacterState.AttackStartBurst, CharacterState.AttackingBurst);
			SetTransformCondition(CharacterState.AttackStartBurst, CharacterState.Hurt);

			SetTransformCondition(CharacterState.AttackStartDash, CharacterState.AttackingDash);
			SetTransformCondition(CharacterState.AttackStartDash, CharacterState.Hurt);

			SetTransformCondition(CharacterState.AttackStartMelee, CharacterState.AttackingMelee);
			SetTransformCondition(CharacterState.AttackStartMelee, CharacterState.Hurt);


			SetTransformCondition(CharacterState.AttackStartShoot, CharacterState.AttackingShoot);
			SetTransformCondition(CharacterState.AttackStartShoot, CharacterState.Hurt);

			SetTransformCondition(CharacterState.AttackStartThrow, CharacterState.AttackingThrow);
			SetTransformCondition(CharacterState.AttackStartThrow, CharacterState.Hurt);
		}
	}
}
