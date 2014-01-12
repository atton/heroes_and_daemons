using System;
using UnityEngine;
using GameSystem;
using GameSystem.SettingDefinition;

namespace CharacterSkill {

	public class SkillControl {
		private int coolingSkillFrame;
		private int needCoolFrame;

		private Skill skill;

		public SkillControl(CharacterParameter characterParameter, Skill s) {
			needCoolFrame     = characterParameter.GetCoolDownFrameFromSkill(s);
			coolingSkillFrame = needCoolFrame;
			skill             = s;
			}

		/* public methods */
		public void InclementCoolingFrame() {
			if (coolingSkillFrame < needCoolFrame) coolingSkillFrame++;
		}

		public bool IsSkillAvailable() {
			return coolingSkillFrame == needCoolFrame;
		}

		public void Used() {
			coolingSkillFrame = 0;
		}

		/* getter */
		public Skill Skill {
			get { return this.skill; }
		}
	}

}
