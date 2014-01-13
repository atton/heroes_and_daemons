using System;
using CharacterInterface;

public class SlayerParameter : CharacterParameter {

	public SlayerParameter() {
		hitPoint           = MaxHitPoint;
		maxHitPoint        = 100;
		runSpeed           = 20.0f;
		CoolDownFrameShoot = 100;
		CoolDownFrameMelee = 150;
	}

	public override void Damage(DamageInfo info)	{
		hitPoint -= info.DamageValue();
	}
}
