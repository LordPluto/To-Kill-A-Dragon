using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuButtonClick : MonoBehaviour {
	private MenuController enterMenu;

	void Start () {
		enterMenu = GameObject.Find ("Enter Menu").GetComponent<MenuController> ();
	}

	public void StatusClick() {
		enterMenu.SwitchWindow (MenuWindow.StatusWindow);
	}

	public void InventoryClick() {
		enterMenu.SwitchWindow (MenuWindow.InventoryWindow);
	}

	public void EquipmentClick() {
		enterMenu.SwitchWindow (MenuWindow.EquipmentWindow);
	}

	public void CustomizeClick() {
		enterMenu.SwitchWindow (MenuWindow.CustomizeWindow);
	}

	public void QuestClick() {
		enterMenu.SwitchWindow (MenuWindow.QuestWindow);
	}

	public void DebugClick() {
		enterMenu.SwitchWindow (MenuWindow.DebugWindow);
	}

	public void DebugTemp(int index){
		SceneManager.LoadScene (index);
	}

	public void DebugTemp(string levelName){
		SceneManager.LoadScene (levelName);
	}
}
