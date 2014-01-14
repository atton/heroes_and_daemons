using System;
using CharacterInterface;

public class DMascotParameter : CharacterParameter {

	public DMascotParameter() {
		maxHitPoint = 2000;
		hitPoint    = MaxHitPoint;
	}

	public override void Damage(DamageInfo info)	{
		hitPoint -= info.DamageValue();
	}
}
