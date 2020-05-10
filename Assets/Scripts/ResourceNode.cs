using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ResourceNode : MonoBehaviour, IInteractable
{
	[SerializeField] private string resourceName = "";      // The name of the resource (I.E. Wood, Stone, Iron ,etc.)
	[SerializeField] private int resourceAmount = default;  // How many resources are left on this ResourceNode.

	/// <summary>
	/// Checks if the Resource Node amount is above zero
	/// </summary>
	/// <returns>Return True when not empty.</returns>
	public bool HasResources()
	{
		return resourceAmount > 0;
	}

	/// <summary>
	/// Handles the Interaction between the Agent and the Resource Node.
	/// </summary>
	/// <param name="agent"></param>
	/// <param name="amount"></param>
	public void Interact(Agent agent, int amount)
	{
		if(!HasResources())
			Destroy(gameObject);

		Debug.Log(agent.name + " Is interacting with: " + gameObject.name);

		resourceAmount -= amount;
		agent.ResourceInventory += amount;
		agent.CollectedResourceName = resourceName;
	}
}
