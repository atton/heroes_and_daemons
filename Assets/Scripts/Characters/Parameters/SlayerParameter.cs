using System;
using CharacterInterface;

public class SlayerParameter : CharacterParameter {

	public SlayerParameter() {
		character          = GameSystem.SettingDefinition.Character.Slayer;
		maxHitPoint        = 5000;
		runSpeed           = 20.0f * 1; // mass is 1
		CoolDownFrameShoot = 100;
		CoolDownFrameMelee = 150;
		CoolDownFrameThrow = 300;
		CoolDownFrameDash  = 200;

		hitPoint           = MaxHitPoint;
	}

	public override void Damage(DamageInfo info)	{
		hitPoint -= info.DamageValue();
	}
}
