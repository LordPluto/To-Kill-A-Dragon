using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuickSelectController : MonoBehaviour {

	private Canvas parentCanvas;
	private HUDController imageControl;

	private bool IsActive = false;

	private GameController gameControl;

	public Image IconQ;
	public Image IconE;
	public Image IconSpace;

	public TripleSelect setQ;
	public TripleSelect setE;
	public TripleSelect setSpace;

	// Use this for initialization
	void Start () {
		imageControl = GameObject.Find ("HUD Canvas").GetComponent<HUDController> ();
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
	}

	void Awake () {
		parentCanvas = GetComponentInParent<Canvas> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			parentCanvas.enabled = true;
			IsActive = true;
			gameControl.SwitchingSpells (SpellSelectStyle.Quick, true);
		} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
			parentCanvas.enabled = false;
			IsActive = false;
			gameControl.SwitchingSpells (SpellSelectStyle.Quick, false);
		}

		if (IsActive) {
			if (Input.GetKeyDown (KeyCode.Q)) {
				setQ.EquipSelections ();
				SetCenter (setQ.SpellQ, setQ.SpellE, setQ.SpellSpace);
			} else if (Input.GetKeyDown (KeyCode.E)) {
				setE.EquipSelections ();
				SetCenter (setE.SpellQ, setE.SpellE, setE.SpellSpace);
			} else if (Input.GetKeyDown (KeyCode.Space)) {
				setSpace.EquipSelections ();
				SetCenter (setSpace.SpellQ, setSpace.SpellE, setSpace.SpellSpace);
			}
		}
	}

	/**
	 * <para>Sets the icons in the center</para>
	 * <param name="q">Spell to put in Q slot</param>
	 * <param name="e">Spell to put in E slot</param>
	 * <param name="space">Spell to put in Space slot</param>
	 * **/
	public void SetCenter (SpellNumber q, SpellNumber e, SpellNumber space) {
		IconQ.sprite = imageControl.Spells [(int)q];
		IconE.sprite = imageControl.Spells [(int)e];
		IconSpace.sprite = imageControl.Spells [(int)space];
	}

	public Sprite GetSprite (SpellNumber s) {
		return imageControl.Spells [(int)s];
	}
}
