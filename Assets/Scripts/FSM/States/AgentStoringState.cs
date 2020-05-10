using RayWenderlich.Unity.StatePatternInUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentStoringState : State
{
	private float interactionInterval;

	public AgentStoringState(Agent agent, StateMachine stateMachine) : base(agent, stateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();

		Debug.Log("Starting to Interact!");
		interactionInterval = agent.InteractionInterval;
		agent.AgentState = AgentState.Storing;
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();
		if(!agent.Target || agent.ResourceInventory <= 0)
		{
			agent.Target = null;
			agent.AgentState = AgentState.Collecting;
			stateMachine.ChangeState(agent.SearchingState);
		}

		if(agent.Target && !agent.Target.GetComponent<ResourceStorage>().HasSpace())
		{
			agent.Target = null;
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
