using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///	a VERY simple Interface.
/// </summary>
public interface IInteractable
{
	void Interact(Agent agent, int amount);
}
