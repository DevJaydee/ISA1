using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBuilding : MonoBehaviour
{
	[SerializeField] private GameObject agentPrefab = default;  // Which agent to spawn form this building.
	[SerializeField] private int maxAgents = 1;					// How many agents belong to this building.
}
