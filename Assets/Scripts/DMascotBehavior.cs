using UnityEngine;
using System.Collections;
using CharacterInterface;

public class DMascotBehavior : MonoBehaviour, IDamage {

	public  int MaxHitPoint = 100;
	private int hitPoint;

	public void Damage(DamageInfo info) {
		hitPoint -= info.DamageValue();

		if (hitPoint <= 0) {
			// TODO: Add mini effect?
			Destroy(gameObject);
		}
	}

	void OnGUI() {
		string hitPointStr = hitPoint.ToString() + "/" + MaxHitPoint.ToString();
		GUI.Label(new Rect(20, 20, 100, 20), hitPointStr);	// show HP on left top
	}

	// Use this for initialization
	void Start () {
		hitPoint = MaxHitPoint;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
