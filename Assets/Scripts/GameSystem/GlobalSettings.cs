using System;
using GameSystem.SettingDefinition;

/* global settings singleton */

namespace GameSystem {

	public class GlobalSettings	{
		/* definitions */
		private const Character kDefaultCharacter = Character.Slayer;

		/* singleton */
		private static GlobalSettings setting = new GlobalSettings();

		private GlobalSettings () {
			character = kDefaultCharacter;
		}

		public static GlobalSettings Setting {
			get { return setting; }
		}

		/* variables */
		private Character character;

		/* getter setter */
		public Character Character {
			set { this.character = value; }
			get { return this.character; }
		}
	}
}
