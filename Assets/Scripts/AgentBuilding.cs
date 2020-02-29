using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBuilding : MonoBehaviour
{
	#region Variables
	[SerializeField] private GameObject agentPrefab = default;  // Which agent to spawn form this building.
	[SerializeField] private int maxAgents = 1;                 // How many agents belong to this building.
	[Space]
	[SerializeField] private List<GameObject> activeAgents = new List<GameObject>();    // How many agents are active.
	#endregion

	#region Getters And Setters
	public GameObject AgentPrefab { get => agentPrefab; set => agentPrefab = value; }
	public int MaxAgents { get => maxAgents; set => maxAgents = value; }
	public List<GameObject> ActiveAgents { get => activeAgents; set => activeAgents = value; }
	#endregion

	#region Monobehaviour Callbacks
	private void Start()
	{
		SpawnAgents();
	}
	#endregion

	#region Spawning Agents
	private void SpawnAgents()
	{
		for(int i = 0; i < maxAgents; i++)
		{
			Instantiate(agentPrefab, transform.position, Quaternion.identity);
		}
	}
	#endregion
}
