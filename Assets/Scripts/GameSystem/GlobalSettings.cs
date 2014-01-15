using System;
using GameSystem.SettingDefinition;

/* global settings singleton */

namespace GameSystem {

	public class GlobalSettings {
		/* definitions */
		private const Character kDefaultCharacter = Character.Slayer;
		private const Skill     kDefaultSkillA    = Skill.Throw;
		private const Skill     kDefaultSkillB    = Skill.Shoot;
		private const Skill     kDefaultSkillC    = Skill.Dash;

		/* singleton */
		private static GlobalSettings setting = new GlobalSettings();

		private GlobalSettings () {
			character = kDefaultCharacter;
			skillA    = kDefaultSkillA;
			skillB    = kDefaultSkillB;
			skillC    = kDefaultSkillC;
		}

		public static GlobalSettings Setting {
			get { return setting; }
		}

		/* variables */
		private Character character;
		private Skill     skillA;
		private Skill     skillB;
		private Skill     skillC;

		/* getter setter */
		public Character Character {
			set { this.character = value; }
			get { return this.character; }
		}

		public Skill SkillA {
			set { this.skillA = value; }
			get { return this.skillA; }
		}

		public Skill SkillB {
			set { this.skillB = value; }
			get { return this.skillB; }
		}

		public Skill SkillC {
			set { this.skillC = value; }
			get { return this.skillC; }
		}

	}
}
