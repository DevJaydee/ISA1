using RayWenderlich.Unity.StatePatternInUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentIdleState : State
{
	public AgentIdleState(Agent agent, StateMachine stateMachine) : base(agent, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();

		Debug.Log("I'm just idle!");
		agent.AgentState = AgentState.Idle;
		stateMachine.ChangeState(agent.SearchingState);
	}

	public override void Exit()
	{
		base.Exit();
	}
	/// <summary>
	/// Handles all the logic updates.
	/// </summary>
	public override void LogicUpdate()
	{
		base.LogicUpdate();
	}
}
