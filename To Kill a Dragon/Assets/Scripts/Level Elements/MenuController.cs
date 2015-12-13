using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	private Canvas menu;
	private Canvas buttons;

	private GameController gameControl;
	private Text walletText;

	private Image debugImage;
	private Button debugButton;
	private Text debugText;

	private bool objInit = false;
	private bool debugToggle = false;

	private string[] keys = {"1","1","1","6"};
	private int keyIndex=0;

	// Use this for initialization
	void Start () {
		InitObjects ();
		DontDestroyOnLoad (GameObject.Find ("MenuButton Canvas"));
	}

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
				if (Input.GetKeyDown (keys [keyIndex]))
						keyIndex++;

				if (keyIndex >= keys.Length) {
						keyIndex = 0;
						ToggleDebug (!debugToggle);
				}
		}

	/**
	 * <para>Initializes all the objects.</para>
	 * */
	private void InitObjects() {
				menu = GameObject.Find ("Enter Menu").GetComponent<Canvas> ();
				buttons = GameObject.Find ("MenuButton Canvas").GetComponent<Canvas> ();
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		walletText = GameObject.Find ("Menu Money").GetComponent<Text> ();

				GameObject debug = GameObject.Find ("DebugButton");
		
				debugImage = debug.GetComponent<Image> ();
				debugButton = debug.GetComponent<Button> ();
				debugText = debug.GetComponentInChildren<Text> ();

				objInit = true;
		}

	/**
	 * <para>Enables/Disables the enter menu items</para>
	 * <param name="enableContents">True to enable contents, false to disable them</param>
	 * **/
	public void SetEnabled(bool enableContents) {
				if (!objInit) {
						InitObjects ();
				}
				menu.enabled = enableContents;
				buttons.enabled = enableContents;

				walletText.text = gameControl.GetWallet ().ToString();
		}

	/**
	 * <para>Enables debug mode. If debug mode is enabled, the debug menu option
	 * will be shown.</para>
	 * <param name="EnableDebug">Enable or disable debug mode.</param>
	 * **/
	public void ToggleDebug(bool EnableDebug) {
				debugToggle = EnableDebug;

				debugImage.enabled = EnableDebug;
				debugButton.enabled = EnableDebug;
				debugText.enabled = EnableDebug;
		}

	/**
	 * <para>Sets the sequence for toggling debug mode</para>
	 * <param name="KeySequence">Key sequence in array form.</param>
	 * **/
	public void SetKeySequence(string[] KeySequence) {
				keys = KeySequence;
				keyIndex = 0;
		}
}
