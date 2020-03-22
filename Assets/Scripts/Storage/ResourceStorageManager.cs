using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorageManager : MonoBehaviour
{
	#region Variables
	private static ResourceStorageManager instance = null;

	[Header("Total Stored Items:")]
	[SerializeField] private ResourceItem wood = default;
	[SerializeField] private ResourceItem stone = default;
	[SerializeField] private ResourceItem iron = default;
	[SerializeField] private ResourceItem food = default;

	[Header("Storage Objects In Scene:")]
	[SerializeField] private List<ResourceStorage> resourceStoragesInScene = new List<ResourceStorage>();
	#endregion

	#region Getters And Setters
	public static ResourceStorageManager Instance { get => instance; }

	public List<ResourceStorage> ResourceStoragesInScene { get => resourceStoragesInScene; set => resourceStoragesInScene = value; }

	public ResourceItem Wood { get => wood; set => wood = value; }
	public ResourceItem Stone { get => stone; set => stone = value; }
	public ResourceItem Iron { get => iron; set => iron = value; }
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
