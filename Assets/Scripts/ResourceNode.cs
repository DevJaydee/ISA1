using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ResourceNode : MonoBehaviour, IInteractable
{
	[SerializeField] private string resourceName = "";      // The name of the resource (I.E. Wood, Stone, Iron ,etc.)
	[SerializeField] private int resourceAmount = default;  // How many resources are left on this ResourceNode.

	public bool HasResources()
	{
		return resourceAmount > 0;
	}

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
