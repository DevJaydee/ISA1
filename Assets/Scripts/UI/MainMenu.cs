using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private GameObject MainMenuGameObject = default;       // Reference to the Main Menu GameObject.
	[SerializeField] private GameObject SettingsMenuGameObject = default;   // Reference to the Settings Menu Object.
	[SerializeField] private GameObject CreditsMenuGameObject = default;    // Reference to the Settings Menu Object.

	/// <summary>
	/// Starts the main game scene.
	/// </summary>
	public void StartGame()
	{
		SceneManager.LoadScene(1);
	}

	/// <summary>
	/// Quits the application.
	/// </summary>
	public void QuitGame()
	{
		Application.Quit();
	}

	/// <summary>
	/// Opens the Settings Menu.
	/// </summary>
	public void ToggleSettingsMenu()
	{
		MainMenuGameObject.SetActive(!MainMenuGameObject.activeInHierarchy);
		SettingsMenuGameObject.SetActive(!SettingsMenuGameObject.activeInHierarchy);
	}

	/// <summary>
	/// Opens the Credits Menu.
	/// </summary>
	public void ToggleCreditsMenu()
	{
		MainMenuGameObject.SetActive(!MainMenuGameObject.activeInHierarchy);
		CreditsMenuGameObject.SetActive(!CreditsMenuGameObject.activeInHierarchy);
	}
}
