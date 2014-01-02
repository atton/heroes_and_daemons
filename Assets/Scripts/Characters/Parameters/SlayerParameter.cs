using System;
using CharacterInterface;

public class SlayerParameter : CharacterParameter {
	
	public SlayerParameter() {
		maxHitPoint = 100;
		hitPoint    = MaxHitPoint;
		runSpeed    = 20.0f;
	}
	
	public override void Damage(DamageInfo info)	{
		hitPoint -= info.DamageValue();
	}
}
