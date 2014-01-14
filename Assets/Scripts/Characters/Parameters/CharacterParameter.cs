using System;
using UnityEngine;
using CharacterInterface;
using GameSystem.SettingDefinition;

public class CharacterParameter : IDamage {

	/* parameters */
	protected Character character;

	protected int maxHitPoint;
	protected int hitPoint;
	protected int superArmerDamageLimit;

	protected float runSpeed;

	/* skill cool down frame */
	protected int CoolDownFrameShoot;
	protected int CoolDownFrameMelee;

	/* variables for show HP */
	const int kLabelSpace = 20;
	const int kLabelWidth = 200;

	private string hitPointStr;
	private Rect   hitPointLabel = new Rect(kLabelSpace, kLabelSpace, kLabelWidth, kLabelSpace);

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

	public void ShowHitPoint() {
		hitPointStr = "";
		hitPointStr += character.ToString();
		hitPointStr += " : ";
		hitPointStr	+= hitPoint.ToString();
		hitPointStr += "/";
		hitPointStr += maxHitPoint.ToString();

		GUI.Label(hitPointLabel, hitPointStr);	// show HP on left top
	}

	/* Getter */
	public int MaxHitPoint {
		get { return this.maxHitPoint; }
	}

	public int HitPoint {
		get { return this.hitPoint; }
	}

	public int SuperArmerDamageLimit {
		get { return this.superArmerDamageLimit; }
	}

	public float RunSpeed {
		get { return this.runSpeed; }
	}

}
