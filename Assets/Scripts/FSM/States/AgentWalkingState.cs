using RayWenderlich.Unity.StatePatternInUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWalkingState : State
{
	private float agentDestinationSetInterval;

	public AgentWalkingState(Agent agent, StateMachine stateMachine) : base(agent, stateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();

		Debug.Log("Starting to walk!");
		agent.AgentState = AgentState.Walking;
		agentDestinationSetInterval = agent.AgentDestinationSetInterval;
		agent.NavMeshAgent.isStopped = false;
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if(!agent.Target)
			stateMachine.ChangeState(agent.SearchingState);

		agentDestinationSetInterval -= Time.deltaTime;

		if(agentDestinationSetInterval <= 0)
		{
			agent.NavMeshAgent.SetDestination(agent.Target.position);
			agentDestinationSetInterval = agent.AgentDestinationSetInterval;
		}

		if(Vector3.Distance(agent.transform.position, agent.Target.position) <= agent.InteractionRadius)
		{
			agent.NavMeshAgent.isStopped = true;

			switch(agent.AgentState)
			{
				case AgentState.Collecting:
					stateMachine.ChangeState(agent.CollectingState);
					break;

				case AgentState.Storing:
					stateMachine.ChangeState(agent.StoringState);
					break;

				default:
					break;
			}
		}
	}
}
