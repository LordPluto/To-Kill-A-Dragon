using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	private RawImage background;
	private Image display;

	private Image statusImage;
	private Button statusButton;
	private Text statusText;
	private Image inventoryImage;
	private Button inventoryButton;
	private Text inventoryText;
	private Image equipmentImage;
	private Button equipmentButton;
	private Text equipmentText;
	private Image customizeImage;
	private Button customizeButton;
	private Text customizeText;
	private Image questImage;
	private Button questButton;
	private Text questText;

	private Image debugImage;
	private Button debugButton;
	private Text debugText;

	private bool objInit = false;

	// Use this for initialization
	void Start () {
		InitObjects ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * <para>Initializes all the objects.</para>
	 * */
	private void InitObjects() {
				background = GameObject.Find ("Background Panel").GetComponent<RawImage> ();
				display = GameObject.Find ("Display Panel").GetComponent<Image> ();
		
				GameObject status = GameObject.Find ("StatusButton");
				GameObject inventory = GameObject.Find ("InventoryButton");
				GameObject equipment = GameObject.Find ("EquipmentButton");
				GameObject customize = GameObject.Find ("CustomizeButton");
				GameObject quest = GameObject.Find ("QuestButton");
				GameObject debug = GameObject.Find ("DebugButton");
		
				statusImage = status.GetComponent<Image> ();
				statusButton = status.GetComponent<Button> ();
				statusText = status.GetComponentInChildren<Text> ();
		
				inventoryImage = inventory.GetComponent<Image> ();
				inventoryButton = inventory.GetComponent <Button> ();
				inventoryText = inventory.GetComponentInChildren<Text> ();
		
				equipmentImage = equipment.GetComponent<Image> ();
				equipmentButton = equipment.GetComponent<Button> ();
				equipmentText = equipment.GetComponentInChildren<Text> ();
		
				customizeImage = customize.GetComponent<Image> ();
				customizeButton = customize.GetComponent<Button> ();
				customizeText = customize.GetComponentInChildren<Text> ();
		
				questImage = quest.GetComponent<Image> ();
				questButton = quest.GetComponent<Button> ();
				questText = quest.GetComponentInChildren<Text> ();
		
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
				background.enabled = enableContents;
				display.enabled = enableContents;

				statusImage.enabled = enableContents;
				statusButton.enabled = enableContents;
				statusText.enabled = enableContents;

				inventoryImage.enabled = enableContents;
				inventoryButton.enabled = enableContents;
				inventoryText.enabled = enableContents;

				equipmentImage.enabled = enableContents;
				equipmentButton.enabled = enableContents;
				equipmentText.enabled = enableContents;

				customizeImage.enabled = enableContents;
				customizeButton.enabled = enableContents;
				customizeText.enabled = enableContents;

				questImage.enabled = enableContents;
				questButton.enabled = enableContents;
				questText.enabled = enableContents;

				debugImage.enabled = enableContents;
				debugButton.enabled = enableContents;
				debugText.enabled = enableContents;
		}
}
