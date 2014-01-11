using System;
using CharacterInterface;
using GameSystem.SettingDefinition;

public class CharacterParameter : IDamage {
	/* parameters */
	protected int maxHitPoint;
	protected int hitPoint;

	protected float runSpeed;

	/* skill cool down frame */
	protected int CoolDownFrameShoot;
	protected int CoolDownFrameMelee;

	virtual public void Damage(DamageInfo info) {
		throw new SystemException("CharacterParameter#Damage is virtual. please override it");
	}

	/* public methods */
	public int GetCoolDownFrameFromSkill(Skill s) {
		switch (s) {

		case Skill.Shoot:
			return CoolDownFrameShoot;

		case Skill.Melee:
			return CoolDownFrameMelee;
		
		default:
				throw new UnityEngine.UnityException("Undefined cool down frame for : " + s.ToString());
		}
	}

	/* Getter */
	public int MaxHitPoint {
		get { return this.maxHitPoint; }
	}

	public int HitPoint {
		get { return this.hitPoint; }
	}

	public float RunSpeed {
		get { return this.runSpeed; }
	}

}
