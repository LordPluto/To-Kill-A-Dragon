using UnityEngine;
using System.Collections;

public interface StopOnTalk {
	void TalkingFreeze();
	void TalkingMove();
	void NotifyControllerOnTalk();
}

public interface StopOnFreeze {
	void Freeze();
	void Move();
	void NotifyControllerOnFreeze();
}

public interface StopOnCutscene {
	void CutsceneFreeze();
	void CutsceneMove();
	void NotifyControllerOnCutscene();
}

public interface CutsceneParticipant {
	void SetPath(Transform[] PathPoints, CutscenePathManager PathManager);
}