using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorageManager : MonoBehaviour
{
	#region Variables
	private static ResourceStorageManager instance = null;	// An instance of the Resource Storage Manager

	[Header("Total Stored Items:")]
	[SerializeField] private ResourceItem wood = default;		// Reference to the Wood Resource Item.
	[SerializeField] private ResourceItem stone = default;      // Reference to the Stone Resource Item.
	[SerializeField] private ResourceItem food = default;       // Reference to the Food Resource Item.

	[Header("Storage Objects In Scene:")]
	[SerializeField] private List<ResourceStorage> resourceStoragesInScene = new List<ResourceStorage>();	// List with all the Storages in the scene.
	#endregion

	#region Properties
	public static ResourceStorageManager Instance { get => instance; }

	public List<ResourceStorage> ResourceStoragesInScene { get => resourceStoragesInScene; set => resourceStoragesInScene = value; }

	public ResourceItem Wood { get => wood; set => wood = value; }
	public ResourceItem Stone { get => stone; set => stone = value; }
	public ResourceItem Food { get => food; set => food = value; }
	#endregion

	#region Monobehaviour Callbacks
	private void Awake()
	{
		if(!instance || instance != this)
			instance = this;
	}
	#endregion
}
