using UnityEngine;
using System.Collections;
using StateMachine;

public class CharacterAnimation : MonoBehaviour {

	protected CharacterState preState;
	protected CharacterState nowState;

	private   CharacterState sentState;
	private   string         lastPlayingAnimationName;

	protected virtual void Start () {
		// initialize invalid state
		sentState = (CharacterState)(-3);
		preState  = (CharacterState)(-2);
		nowState  = (CharacterState)(-1);
		
	}

	protected void PlayAnimation(string animationName) {
		lastPlayingAnimationName = animationName;
		animation.Play(animationName);
	}

	public virtual void PlayAnimationFromState(CharacterState cs) {
		preState = nowState;
		nowState = cs;
	}

	public bool IsFinishedNowAnimation() {
		return !animation.IsPlaying(lastPlayingAnimationName);
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		if (networkView.isMine && stream.isWriting && (sentState != nowState)) {
			int state = (int)nowState;
			stream.Serialize(ref state);
			sentState = nowState;
			return;
		}

		if (stream.isReading) {
			int state = -1;		// dummy parameter for receive variable
			stream.Serialize(ref state);
			PlayAnimationFromState((CharacterState)state);
			return;
		}
	}
}
