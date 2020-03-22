using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour, IInteractable
{
	#region Variables
	[SerializeField] private int maxItemsToBeStored = 100;  // How many items can be stored. (This can be of any type)
	[SerializeField] private Dictionary<string, int> itemsStored = new Dictionary<string, int>();   // A dictionary with all the resources stored.
	[SerializeField] private List<Item> itemsStoredSerialized = new List<Item>();   // List with all the stored items but serialized.
	#endregion

	#region Getters & Setters

	#endregion

	public void Interact(Agent agent, int amount)
	{
		if(itemsStored.ContainsKey(agent.CollectedResourceName))
		{
			itemsStored[agent.CollectedResourceName] += amount;
			for(int i = 0; i < itemsStoredSerialized.Count; i++)
				if(itemsStoredSerialized[i].name == agent.CollectedResourceName)
					itemsStoredSerialized[i].Amount += amount;
		}
		else
		{
			itemsStored.Add(agent.CollectedResourceName, amount);

			Item newItem = new Item
			{
				name = agent.CollectedResourceName,
				Amount = amount
			};
			itemsStoredSerialized.Add(newItem);
		}

		agent.ResourceInventory -= amount;
	}

	public bool HasSpace()
	{
		int totalItemsStored = 0;
		for(int i = 0; i < itemsStored.Count; i++)
		{
		}

		return totalItemsStored <= maxItemsToBeStored ? true : false;
	}
}

[System.Serializable]
public class Item : ResourceStorage
{
	[SerializeField] private new string name = default;   // Name of the Item.
	[SerializeField] private int amount = default;    // Amount of the Item.

	public string Name { get => name; set => name = value; }
	public int Amount { get => amount; set => amount = value; }
}