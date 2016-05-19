using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuButtonClick : MonoBehaviour {
	private MenuController enterMenu;

	public MenuButton[] buttons;
	private MenuButton currentButton;

	void Start () {
		enterMenu = GameObject.Find ("Enter Menu").GetComponent<MenuController> ();
		currentButton = buttons [0];
		currentButton.OnWindowSwitch (true);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.X)) {
			currentButton.OnWindowSwitch (false);
			EventSystem.current.SetSelectedGameObject (currentButton.GetPartner ().gameObject);
		}
	}

	public void StatusClick() {
		SwitchWindow (MenuWindow.StatusWindow);
	}

	public void InventoryClick() {
		SwitchWindow (MenuWindow.InventoryWindow);
	}

	public void EquipmentClick() {
		SwitchWindow (MenuWindow.EquipmentWindow);
	}

	public void CustomizeClick() {
		SwitchWindow (MenuWindow.CustomizeWindow);
	}

	public void QuestClick() {
		SwitchWindow (MenuWindow.QuestWindow);
	}

	public void DebugClick() {
		SwitchWindow (MenuWindow.DebugWindow);
	}

	private void SwitchWindow (MenuWindow selectedWindow) {
		EventSystem.current.SetSelectedGameObject (null);

		enterMenu.SwitchWindow (selectedWindow);

		currentButton.OnWindowSwitch (false);
		currentButton = buttons [(int)selectedWindow];
		currentButton.OnWindowSwitch (true);
	}
}
