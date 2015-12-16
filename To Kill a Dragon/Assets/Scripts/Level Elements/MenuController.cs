using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public enum MenuWindow {
	StatusWindow,
	InventoryWindow,
	EquipmentWindow,
	CustomizeWindow,
	QuestWindow,
	DebugWindow
}

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

	void OnLevelWasLoaded(int level) {
		if (SceneManager.GetActiveScene ().name.Equals ("Loading Screen")) {
			this.enabled = false;
		} else {
			this.enabled = true;
		}
	}

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			if (Input.GetKeyDown (keys [keyIndex])) {
				keyIndex++;
			} else {
				keyIndex = 0;
			}
		}

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
	 * <para>Enables debug button. If debug mode is enabled, the debug menu option
	 * will be shown.</para>
	 * <param name="EnableDebug">Enable or disable debug button.</param>
	 * **/
	public void ToggleDebug(bool EnableDebug) {
		if (Debug.isDebugBuild) {
			debugToggle = EnableDebug;

			debugImage.enabled = EnableDebug;
			debugButton.enabled = EnableDebug;
			debugText.enabled = EnableDebug;
		}
	}

	/**
	 * <para>Sets the sequence for toggling debug mode</para>
	 * <param name="KeySequence">Key sequence in array form.</param>
	 * **/
	public void SetKeySequence(string[] KeySequence) {
		keys = KeySequence;
		keyIndex = 0;
	}

	/**
	 * <para>Switches to the given menu window.</para>
	 * <param name="Window">Menu window to show</param>
	 * **/
	public void SwitchWindow(MenuWindow Window) {
		switch (Window) {
		case MenuWindow.StatusWindow:
			break;
		case MenuWindow.InventoryWindow:
			break;
		case MenuWindow.EquipmentWindow:
			break;
		case MenuWindow.CustomizeWindow:
			break;
		case MenuWindow.QuestWindow:
			break;
		case MenuWindow.DebugWindow:
			break;
		default:
			break;
		}
	}
}
