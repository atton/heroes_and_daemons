using System;
using CharacterInterface;

public class GolemParameter : CharacterParameter {

	public GolemParameter() {
		hitPoint           = MaxHitPoint;
		maxHitPoint        = 200;
		runSpeed           = 5.0f;
		CoolDownFrameShoot = 300;
		CoolDownFrameMelee = 450;
	}

	public override void Damage(DamageInfo info)	{
		hitPoint -= info.DamageValue();
	}
}
