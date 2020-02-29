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
		agentDestinationSetInterval = agent.AgentDestinationSetInterval;
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		agentDestinationSetInterval -= Time.deltaTime;

		if(agentDestinationSetInterval <= 0)
		{
			agent.NavMeshAgent.SetDestination(agent.Target.position);
			agentDestinationSetInterval = agent.AgentDestinationSetInterval;
		}

		if(Vector3.Distance(agent.transform.position, agent.Target.position) <= agent.InteractionRadius)
		{
			agent.NavMeshAgent.isStopped = true;
			stateMachine.ChangeState(agent.InteractionState);
		}
	}
}
