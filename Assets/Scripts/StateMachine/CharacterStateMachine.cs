using System;
using System.Collections;

namespace StateMachine {
	public class CharacterStateMachine {

		CharacterState nowState;
		CharacterState preState;
		CharacterState defaultState;

		Hashtable transformConditions;

		int frameCount;

		public CharacterStateMachine(CharacterState cs) {
			nowState     = cs;
			defaultState = cs;
			frameCount   = -1;

			transformConditions = new Hashtable();
		}

		public void SetTransformCondition(CharacterState beforeState, CharacterState nextState) {
			if (!transformConditions.ContainsKey(beforeState)) {
				transformConditions.Add(beforeState, new ArrayList());
			}

			(transformConditions[beforeState] as ArrayList).Add(nextState);
		}

		public bool TryTransform(CharacterState tryNextState) {
			if (!transformConditions.Contains(nowState)) return false;
			if (!(transformConditions[nowState] as ArrayList).Contains(tryNextState)) return false;

			preState   = nowState;
			nowState   = tryNextState;
			frameCount = -1;
			return true;
		}

		public void EndNowState() {
			preState   = nowState;
			nowState   = defaultState;
			frameCount = -1;
		}

		public void UpdateFrameCount() {
			frameCount++;
		}

		/* Getter and Setter */
		public CharacterState PreState() {
			return preState;
		}
		public CharacterState NowState() {
			return nowState;
		}
		public int FrameCount() {
			return frameCount;
		}
	}
}