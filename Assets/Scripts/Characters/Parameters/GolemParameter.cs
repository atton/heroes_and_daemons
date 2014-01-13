using System;
using CharacterInterface;

public class GolemParameter : CharacterParameter {

	public GolemParameter() {
		maxHitPoint        = 200;
		hitPoint           = MaxHitPoint;
		runSpeed           = 50.0f;
		CoolDownFrameShoot = 300;
		CoolDownFrameMelee = 450;
	}

	public override void Damage(DamageInfo info)	{
		hitPoint -= info.DamageValue();
	}
}
