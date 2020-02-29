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
				SearchForTarget();
				searchInterval = agent.SearchInterval;
			}
		}
	}

	private void SearchForTarget()
	{
		Collider[] colliders = Physics.OverlapSphere(agent.transform.position, agent.SearchRadius, agent.InteractionMask);
		Collider nearestCollider = null;
		float minSqrDistance = Mathf.Infinity;
		for(int i = 0; i < colliders.Length; i++)
		{
			float sqrDistanceToCenter = (agent.transform.position - colliders[i].transform.position).sqrMagnitude;
			if(sqrDistanceToCenter < minSqrDistance)
			{
				minSqrDistance = sqrDistanceToCenter;
				nearestCollider = colliders[i];
				Debug.Log("Nearest object found!");
			}
		}
		agent.InteractionRadius = nearestCollider.GetComponent<BoxCollider>().size.x * 1.25f;
		agent.Target = nearestCollider.transform;
	}
}
