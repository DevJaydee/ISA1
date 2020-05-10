using RayWenderlich.Unity.StatePatternInUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AgentState
{
	Idle, Searching, Walking, Collecting, Storing
}

public class Agent : MonoBehaviour
{
	#region Variables
	private StateMachine behaviourSM = default;    // Reference to the SFM.
	private AgentIdleState idleState = default;    // Reference to the Agent Idle State.
	private AgentWalkingState walkingState = default;  // Reference to the Agent Walking State.
	private AgentSearchingState searchingState = default;  // Reference to the Agent Searching State.
	private AgentCollectingState collectingState = default;   // Reference to the Agent Interaction State.
	private AgentStoringState storingState = default;           // Reference to the Agent Storing State;

	[SerializeField] private AgentState agentState = AgentState.Idle;
	[Space]
	[SerializeField] private Animator anim = default;       // Reference to the animator component.
	[SerializeField] private Transform target = default;    // The target transform.
	[SerializeField] private NavMeshAgent navMeshAgent = default;   // Reference the NavMeshAgent component.
	[Space]
	[SerializeField] private float movementSpeed = 5f;      // The speed at which the agent will move.
	[SerializeField] private float searchRadius = 75f;      // How far the agent can "detect" resources.
	[SerializeField] private float interactionRadius = 1f;  // How close the agent has to be to a resource node to interact with it.
	[Space]
	[SerializeField] private float searchInterval = 0.1f;   // How often the agent will search per second for resources nearby.
	[SerializeField] private float agentDestinationSetInterval = default; // How often the destination of the NavMeshAgent will be set.
	[SerializeField] private float interactionInterval = 1f;    // How often the agent interacts with an interactable.
	[SerializeField] private LayerMask collectingMask = default;   // A layermask for all the objects the Agent can interact with.
	[Space]
	[SerializeField] private LayerMask interactionStorageMask = default;    // A layermask for all the storgage objects the agent can interact with.
	[SerializeField] private string collectedResourceName = ""; // The name of the collected resource.
	[SerializeField] private int resourceInventory = 0;   // The inventory of the agent that stores all the resources.
	[SerializeField] private int maxResourcesInInventory = 10;  // The max amount of resources in the inventory.
	[SerializeField] private int gatherAmount = 1;  // How much the Agent will remove from the resourceNode, and add to it's own inv.
	[Space]
	[SerializeField] private Material[] skinMaterials = default;    // all the skin materials this Agent could pick from.
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
	[SerializeField] private SkinnedMeshRenderer renderer = default;   // Reference to the Mesh Renderer component.
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
	#endregion

	#region Getters And Setters
	public AgentState AgentState { get => agentState; set => agentState = value; }

	public AgentIdleState IdleState { get => idleState; set => idleState = value; }
	public AgentWalkingState WalkingState { get => walkingState; set => walkingState = value; }
	public AgentSearchingState SearchingState { get => searchingState; set => searchingState = value; }
	public AgentCollectingState CollectingState { get => collectingState; set => collectingState = value; }
	public AgentStoringState StoringState { get => storingState; set => storingState = value; }

	public Animator Anim { get => anim; set => anim = value; }
	public Transform Target { get => target; set => target = value; }
	public NavMeshAgent NavMeshAgent { get => navMeshAgent; set => navMeshAgent = value; }

	public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
	public float SearchRadius { get => searchRadius; set => searchRadius = value; }
	public float InteractionRadius { get => interactionRadius; set => interactionRadius = value; }
	public float SearchInterval { get => searchInterval; set => searchInterval = value; }
	public float AgentDestinationSetInterval { get => agentDestinationSetInterval; set => agentDestinationSetInterval = value; }
	public float InteractionInterval { get => interactionInterval; set => interactionInterval = value; }
	public LayerMask CollectingMask { get => collectingMask; set => collectingMask = value; }

	public LayerMask StorageMask { get => interactionStorageMask; set => interactionStorageMask = value; }
	public string CollectedResourceName { get => collectedResourceName; set => collectedResourceName = value; }
	public int ResourceInventory { get => resourceInventory; set => resourceInventory = value; }
	public int MaxResourcesInInventory { get => maxResourcesInInventory; set => maxResourcesInInventory = value; }
	public int GatherAmount { get => gatherAmount; set => gatherAmount = value; }
	#endregion

	#region Monobehaviour Callbacks
	private void Start()
	{
		ChooseRandomSkinMaterial();

		behaviourSM = new StateMachine();

		idleState = new AgentIdleState(this, behaviourSM);
		walkingState = new AgentWalkingState(this, behaviourSM);
		searchingState = new AgentSearchingState(this, behaviourSM);
		CollectingState = new AgentCollectingState(this, behaviourSM);
		storingState = new AgentStoringState(this, behaviourSM);

		navMeshAgent.speed = movementSpeed;

		behaviourSM.Initialize(idleState);
	}

	private void Update()
	{
		behaviourSM.CurrentState.HandleInput();
		behaviourSM.CurrentState.LogicUpdate();

		anim.SetFloat("MovementVelocity", navMeshAgent.velocity.magnitude);

		if(resourceInventory >= maxResourcesInInventory)
			agentState = AgentState.Storing;
		else if(resourceInventory <= maxResourcesInInventory)
			AgentState = AgentState.Collecting;
	}

	private void FixedUpdate()
	{
		behaviourSM.CurrentState.PhysicsUpdate();
	}
	#endregion

	public void SearchForTarget(LayerMask mask)
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, SearchRadius, mask);
		Collider nearestCollider = null;
		float minSqrDistance = Mathf.Infinity;

		// Pick a random target from the list.
		int randTargetIndex = Random.Range(0, colliders.Length);
		target = colliders[randTargetIndex].transform;

		// Pick nearest object
		//for(int i = 0; i < colliders.Length; i++)
		//{
		//	float sqrDistanceToCenter = (transform.position - colliders[i].transform.position).sqrMagnitude;
		//	if(sqrDistanceToCenter < minSqrDistance)
		//	{
		//		minSqrDistance = sqrDistanceToCenter;
		//		nearestCollider = colliders[i];
		//		Debug.Log("Nearest object found!");
		//	}
		//}
		//Target = nearestCollider.transform;

		InteractionRadius = target.GetComponent<BoxCollider>().size.x * 1.25f;
	}

	private void ChooseRandomSkinMaterial()
	{
		int rand = Random.Range(0, skinMaterials.Length);
		renderer.material = skinMaterials[rand];
	}

	#region Debugging stuff
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, searchRadius);

		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, interactionRadius);
	}
	#endregion
}
