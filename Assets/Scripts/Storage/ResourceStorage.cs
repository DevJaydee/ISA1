using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour, IInteractable
{
	#region Variables
	[SerializeField] private int maxItemsToBeStored = 100;  // How many items can be stored. (This can be of any type)
	[SerializeField] private List<ResourceItem> itemsStored = new List<ResourceItem>();   // List with all the stored items but serialized.
	#endregion

	#region Getters & Setters
	public List<ResourceItem> ItemsStored { get => itemsStored; set => itemsStored = value; }
	#endregion

	private void Start()
	{
		ResourceStorageManager.Instance.ResourceStoragesInScene.Add(this);
	}

	private void OnApplicationQuit()
	{
		CleanupStorage();
	}

	public void Interact(Agent agent, int amount)
	{
		for(int i = 0; i < itemsStored.Count; i++)
		{
			if(itemsStored[i].Name == agent.CollectedResourceName)
			{
				int storedAmount = itemsStored[i].AmountStored;
				storedAmount += amount;
				itemsStored[i].AmountStored = storedAmount;

				agent.ResourceInventory -= amount;
			}
		}
	}

	public bool HasSpace()
	{
		int totalItemsStored = 0;

		for(int i = 0; i < itemsStored.Count; i++)
			totalItemsStored += itemsStored[i].AmountStored;

		return totalItemsStored <= maxItemsToBeStored ? true : false;
	}

	private void CleanupStorage()
	{
		foreach(ResourceItem item in itemsStored)
		{
			item.AmountStored = 0;
		}
	}
}