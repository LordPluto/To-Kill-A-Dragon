using UnityEngine;
using System.Collections;

public class SpellBook : MonoBehaviour {

	public Transform[] SpellTransforms;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * <para>Returns the transform associated with a given spell number</para>
	 * <param name="spellNumber">Spell number</param>
	 * **/
	public Transform GetSpellTransform (SpellNumber spellNumber) {
		return SpellTransforms [(int)spellNumber];
	}
}
