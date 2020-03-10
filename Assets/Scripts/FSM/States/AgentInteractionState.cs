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
			if(agent.ResourceInventory > -1)
				agent.Target.GetComponent<IInteractable>()?.Interact(agent, agent.GatherAmount);

			if(agent.ResourceInventory >= agent.MaxResourcesInInventory)
			{
				agent.Target = null;
				stateMachine.ChangeState(agent.SearchingState);
			}

			interactionInterval = agent.InteractionInterval;
		}
	}
}
