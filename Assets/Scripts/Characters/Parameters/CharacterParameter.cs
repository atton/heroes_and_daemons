using System;
using CharacterInterface;

public class CharacterParameter : IDamage {
	protected int maxHitPoint;
	protected int hitPoint;

	protected float runSpeed;

	virtual public void Damage(DamageInfo info) {
		throw new SystemException("CharacterParameter#Damage is virtual. please override it");
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
