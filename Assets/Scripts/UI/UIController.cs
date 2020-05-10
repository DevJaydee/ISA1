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
	[SerializeField] private TextMeshProUGUI storedFoodTextMesh = default;  // Reference to the Stored Food Text Mesh Component.

	private ResourceStorageManager storageManager;
	#endregion

	#region Private Voids
	private void Start()
	{
		storageManager = ResourceStorageManager.Instance;
		StartCoroutine(UpdateUI());
	}
	#endregion

	#region Private IEnumerators
	/// <summary>
	/// Updates the UI at a set interval.
	/// </summary>
	/// <returns></returns>
	private IEnumerator UpdateUI()
	{
		while(true)
		{
			storedWoodTextMesh.text = storageManager.Wood.name + ": " + storageManager.Wood.AmountStored.ToString();
			storedStoneTextMesh.text = storageManager.Stone.name + ": " + storageManager.Stone.AmountStored.ToString();
			storedFoodTextMesh.text = storageManager.Food.name + ": " + storageManager.Food.AmountStored.ToString();

			yield return new WaitForSeconds(updateInterval);
		}
	}
	#endregion
}
