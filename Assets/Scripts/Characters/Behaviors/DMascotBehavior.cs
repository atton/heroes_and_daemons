using UnityEngine;
using System.Collections;
using CharacterInterface;
using GameSystem.GameController;

public class DMascotBehavior : CharacterBehavior {

	void Awake() {
		parameter = new DMascotParameter();
		controller = Object.FindObjectOfType<GameController>();
	}
	
	void OnGUI() {
		string hitPointStr = parameter.HitPoint.ToString() + "/" + parameter.MaxHitPoint.ToString();
		GUI.Label(new Rect(20, 20, 100, 20), hitPointStr);	// show HP on left top
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	override protected void Update () {
		base.Update();
	}

	void focusCamre() {
		// overwrite focusCamera by empty
		// because DMascot is not player
	}

	public override void Damage (DamageInfo info) {
		base.Damage(info);
		if (parameter.HitPoint <= 0) {
			Destroy(gameObject);
			controller.NoticeKnockoutPlayer(networkView.owner);
		}
	}
}
