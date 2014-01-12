using System;
using UnityEngine;

/* controllable interface for PlayerController */

namespace CharacterInterface {

	public interface IControllable {
		void Move(Vector3 moveVector);
	}

}
