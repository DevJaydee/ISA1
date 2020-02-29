using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
	public void Interact(Agent agent)
	{
		Debug.Log(agent.name + " Interacted with " + gameObject.name);
	}
}
