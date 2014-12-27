using UnityEngine;
using System.Collections;

public class PlayerSpellController {

	private float CastTime;
	private float CastDelay;

	public PlayerSpellController() {
				CastTime = -2;
				CastDelay = -1;
		}

	public bool StartCastTime(Spell selectedSpell){
				if (CastTime == -2 && CastDelay == -1) {
						CastTime = selectedSpell.getCastTime ();
						CastDelay = selectedSpell.getDelay ();
						return true;
				}
				return false;
		}

	public float CheckCastTime(){
				if (CastTime > -1) {
						CastTime--;
				} else {
						CastTime = -2;
				}
				return CastTime;
		}

	public float CheckCastDelay() {
				if (CastDelay > 0) {
						CastDelay--;
				} else {
						CastDelay = -1;
				}
				return CastDelay;
		}


	public void Reset(){
				CastTime = -2;
				CastDelay = -1;
		}
}
