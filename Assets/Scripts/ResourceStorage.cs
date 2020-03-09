using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour, IInteractable
{
	#region Variables
	[SerializeField] private int maxItemsToBeStored = 100;  // How many items can be stored. (This can be of any type)
	[SerializeField] private Dictionary<string, int> itemsStored = new Dictionary<string, int>();   // A dictionary with all the resources stored.

	#endregion

	#region Getters & Setters

	#endregion

	public void Interact(Agent agent, int amount)
	{
		if(itemsStored.ContainsKey(agent.CollectedResourceName))
		{
			itemsStored[agent.CollectedResourceName] += amount;
		}
		else
		{
			itemsStored.Add(agent.CollectedResourceName, amount);
		}
		Debug.Log(agent.name + " Stored: " + itemsStored[agent.CollectedResourceName] + " In me.");
		agent.ResourceInventory -= amount;
	}
}
