using System;
using CharacterInterface;

public class SlayerParameter : CharacterParameter {

	public SlayerParameter() {
		maxHitPoint        = 100;
		runSpeed           = 20.0f;
		CoolDownFrameShoot = 100;
		CoolDownFrameMelee = 150;

		hitPoint           = MaxHitPoint;
	}

	public override void Damage(DamageInfo info)	{
		hitPoint -= info.DamageValue();
	}
}
