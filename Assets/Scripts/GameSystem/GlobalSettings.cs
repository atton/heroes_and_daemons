using System;
using GameSystem.SettingDefinition;

/* global settings singleton */

namespace GameSystem {

	public class GlobalSettings	{
		private static GlobalSettings setting = new GlobalSettings();

		private GlobalSettings () {
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
