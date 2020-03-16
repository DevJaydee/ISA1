using RayWenderlich.Unity.StatePatternInUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentCollectingState : State
{
	private float interactionInterval;

	public AgentCollectingState(Agent agent, StateMachine stateMachine) : base(agent, stateMachine)
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
		if(!agent.Target || agent.ResourceInventory >= agent.MaxResourcesInInventory)
		{
			agent.Target = null;
			agent.AgentState = AgentState.Storing;
			stateMachine.ChangeState(agent.SearchingState);
		}

		interactionInterval -= Time.deltaTime;

		if(interactionInterval <= 0)
		{
			agent.Target.GetComponent<IInteractable>()?.Interact(agent, agent.GatherAmount);
			interactionInterval = agent.InteractionInterval;
		}
	}
}
