using System;
using UnityEngine;
using GameSystem.SettingDefinition;

/* controllable interface for PlayerController */

namespace CharacterInterface {

	public interface IControllable {
		void Move(Vector3 moveVector);
		void Jump();
		bool UseSkill(Skill s);
	}

}
