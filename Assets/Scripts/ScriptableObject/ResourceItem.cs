using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceItem", menuName = "ScriptableObjects/New ResourceItem")]
public class ResourceItem : ScriptableObject
{
	[SerializeField] private new string name = "";  // Name of the resource item.
	[SerializeField] private int amountStored = 0;  // The amount of the resource that is stored.

	public string Name { get => name; set => name = value; }
	public int AmountStored { get => amountStored; set => amountStored = value; }
}
