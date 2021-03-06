using System;
using CharacterInterface;

public class GolemParameter : CharacterParameter {

	public GolemParameter() {
		character             = GameSystem.SettingDefinition.Character.Golem;
		maxHitPoint           = 15000;
		superArmerDamageLimit = 500;
		runSpeed              = 15.0f * 3; // mass is 3
		CoolDownFrameShoot    = 300;
		CoolDownFrameMelee    = 450;

		hitPoint = MaxHitPoint;
	}

	public override void Damage(DamageInfo info)	{
		hitPoint -= info.DamageValue();
	}
}
