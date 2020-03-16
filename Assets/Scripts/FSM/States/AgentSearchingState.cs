using RayWenderlich.Unity.StatePatternInUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSearchingState : State
{
	private float searchInterval;

	public AgentSearchingState(Agent agent, StateMachine stateMachine) : base(agent, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();

		Debug.Log("Starting to Search!");
		agent.AgentState = AgentState.Searching;
		searchInterval = agent.SearchInterval;
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if(agent.Target != null)
			stateMachine.ChangeState(agent.WalkingState);
		else
		{
			searchInterval -= Time.deltaTime;
			if(searchInterval <= 0)
			{
				Debug.Log("I'm searching for nearest object!");

				switch(agent.AgentState)
				{
					case AgentState.Collecting:
						agent.SearchForTarget(agent.CollectingMask);
						break;

					case AgentState.Storing:
						agent.SearchForTarget(agent.StorageMask);
						break;

					default:
						break;
				}

				searchInterval = agent.SearchInterval;
			}
		}
	}
}
