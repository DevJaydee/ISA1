using RayWenderlich.Unity.StatePatternInUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
	#region Variables
	private StateMachine behaviourSM = default;    // Reference to the SFM.
	private AgentIdleState idleState = default;    // Reference to the Agent Idle State;
	private AgentWalkingState walkingState = default;  // Reference to the Agent Walking State;
	private AgentSearchingState searchingState = default;  // Reference to the Agent Searching State;
	private AgentInteractionState interactionState = default;   // Reference to the Agent Interaction State;

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
	[SerializeField] private LayerMask interactionMask = default;   // A layermask for all the objects the Agent can interact with.
	#endregion

	#region Getters And Setters
	public AgentIdleState IdleState { get => idleState; set => idleState = value; }
	public AgentWalkingState WalkingState { get => walkingState; set => walkingState = value; }
	public AgentSearchingState SearchingState { get => searchingState; set => searchingState = value; }
	public AgentInteractionState InteractionState { get => interactionState; set => interactionState = value; }

	public Animator Anim { get => anim; set => anim = value; }
	public Transform Target { get => target; set => target = value; }
	public NavMeshAgent NavMeshAgent { get => navMeshAgent; set => navMeshAgent = value; }

	public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
	public float SearchRadius { get => searchRadius; set => searchRadius = value; }
	public float InteractionRadius { get => interactionRadius; set => interactionRadius = value; }
	public float SearchInterval { get => searchInterval; set => searchInterval = value; }
	public float AgentDestinationSetInterval { get => agentDestinationSetInterval; set => agentDestinationSetInterval = value; }
	public float InteractionInterval { get => interactionInterval; set => interactionInterval = value; }
	public LayerMask InteractionMask { get => interactionMask; set => interactionMask = value; }
	#endregion

	#region Monobehaviour Callbacks
	private void Start()
	{
		behaviourSM = new StateMachine();

		idleState = new AgentIdleState(this, behaviourSM);
		walkingState = new AgentWalkingState(this, behaviourSM);
		searchingState = new AgentSearchingState(this, behaviourSM);
		InteractionState = new AgentInteractionState(this, behaviourSM);

		navMeshAgent.speed = movementSpeed;
		navMeshAgent.stoppingDistance = interactionRadius;

		behaviourSM.Initialize(idleState);
	}

	private void Update()
	{
		behaviourSM.CurrentState.HandleInput();
		behaviourSM.CurrentState.LogicUpdate();

		anim.SetFloat("MovementVelocity", navMeshAgent.velocity.magnitude);
	}

	private void FixedUpdate()
	{
		behaviourSM.CurrentState.PhysicsUpdate();
	}
	#endregion

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
