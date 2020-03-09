using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ResourceNode : MonoBehaviour, IInteractable
{
	[SerializeField] private int resourceAmount;

	public bool HasResources()
	{
		return resourceAmount > 0;
	}

	public void Interact(Agent agent, int gatherAmount)
	{
		if(!HasResources())
			return;

		Debug.Log(agent.name + " Is interacting with: " + gameObject.name);

		resourceAmount -= gatherAmount;
		agent.ResourceInventory += gatherAmount;
	}
}
