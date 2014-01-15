using UnityEngine;
using System.Collections;
using CharacterInterface;
using GameSystem.GameController;

public class DMascotBehavior : CharacterBehavior {

	override protected void Awake() {
		parameter = new DMascotParameter();
		gameController = Object.FindObjectOfType<GameController>();
	}

	override protected void OnGUI() {
		string hitPointStr = "DMascot : " + parameter.HitPoint.ToString() + "/" + parameter.MaxHitPoint.ToString();
		GUI.Label(new Rect(220, 20, 200, 20), hitPointStr);	// show HP on left top
	}

	void focusCamre() {
		// overwrite focusCamera by empty
		// because DMascot is not player
	}

	public override void Damage (DamageInfo info) {
		base.Damage(info);
		if (parameter.HitPoint <= 0) {
			Destroy(gameObject);
			gameController.NoticeKnockoutPlayer(networkView.owner);
		}
	}
}
