using RayWenderlich.Unity.StatePatternInUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentInteractionState : State
{
	private float interactionInterval;

	public AgentInteractionState(Agent agent, StateMachine stateMachine) : base(agent, stateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();

		Debug.Log("Starting to Interact!");
		interactionInterval = agent.InteractionInterval;
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		interactionInterval -= Time.deltaTime;

		if(interactionInterval <= 0)
		{
			agent.Target.GetComponent<ResourceNode>().Interact(agent);
			interactionInterval = agent.InteractionInterval;
		}
	}
}
