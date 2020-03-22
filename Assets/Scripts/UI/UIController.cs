using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
	#region Variables
	[Header("Main Settings")]
	[SerializeField] private float updateInterval = 1f; // How many seconds between updating the UI.

	[Header("Resource UI")]
	[SerializeField] private TextMeshProUGUI storedWoodTextMesh = default;  // Reference to the Stored Wood Text Mesh Component.
	[SerializeField] private TextMeshProUGUI storedStoneTextMesh = default; // Reference to the Stored Stone Text Mesh Component.
	[SerializeField] private TextMeshProUGUI storedIronTextMesh = default;  // Reference to the Stored Iron Text Mesh Component.
	[SerializeField] private TextMeshProUGUI storedFoodTextMesh = default;  // Reference to the Stored Food Text Mesh Component.

	private ResourceStorageManager storageManager;
	#endregion

	#region Getters and Setters
	#endregion

	#region MonoBehaviour Callbacks

	#endregion

	#region Private Voids
	private void Start()
	{
		storageManager = ResourceStorageManager.Instance;
		StartCoroutine(UpdateUI());
	}
	#endregion

	#region Private IEnumerators
	private IEnumerator UpdateUI()
	{
		while(true)
		{
			storedWoodTextMesh.text = storageManager.Wood.name + ": " + storageManager.Wood.AmountStored.ToString();
			storedStoneTextMesh.text = storageManager.Stone.name + ": " + storageManager.Stone.AmountStored.ToString();
			storedIronTextMesh.text = storageManager.Iron.name + ": " + storageManager.Iron.AmountStored.ToString();
			storedFoodTextMesh.text = storageManager.Food.name + ": " + storageManager.Food.AmountStored.ToString();

			yield return new WaitForSeconds(updateInterval);
		}
	}
	#endregion

	#region Public Voids

	#endregion
}
