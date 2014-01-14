using System;
using CharacterInterface;

public class DMascotParameter : CharacterParameter {

	public DMascotParameter() {
		character   = GameSystem.SettingDefinition.Character.DMascot;
		maxHitPoint = 2000;
		hitPoint    = MaxHitPoint;
	}

	public override void Damage(DamageInfo info)	{
		hitPoint -= info.DamageValue();
	}
}
