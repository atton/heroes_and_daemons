using System;
namespace CharacterInterface
{
	public class DamageInfo {

		int damageValue;
			
		public DamageInfo() {
		}

		public void SetDamageValue(int val) {
			damageValue = val;
		}

		public int DamageValue() {
			return damageValue;
		}
	}
}
