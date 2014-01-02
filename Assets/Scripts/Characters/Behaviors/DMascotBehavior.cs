using UnityEngine;
using System.Collections;
using CharacterInterface;

public class DMascotBehavior : CharacterBehavior {
	
	void OnGUI() {
		string hitPointStr = hitPoint.ToString() + "/" + MaxHitPoint.ToString();
		GUI.Label(new Rect(20, 20, 100, 20), hitPointStr);	// show HP on left top
	}

	// Use this for initialization
	void Start () {
		hitPoint = MaxHitPoint;

	}
	
	// Update is called once per frame
	override protected void Update () {
		base.Update();
	}

	void focusCamre() {
		// overwrite focusCamera by empty
		// because DMascot is not player
	}
}
