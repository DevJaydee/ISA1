using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
	[SerializeField] private int resourceAmount;

	public void Interact(Agent agent, int gatherAmount)
	{
		Debug.Log(agent.name + " Is interacting with: " + gameObject.name);

		resourceAmount -= gatherAmount;
		agent.ResourceInventory += gatherAmount;
	}

	public bool HasResources()
	{
		return resourceAmount > 0;
	}
}
