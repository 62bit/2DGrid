using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalkToTarget : StateMachineBehaviour
{
    public static Vector3 _target;
    private Builder _builderNPC;
    private Vector3 _home = new Vector3(0f, 0f, 0f);

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _builderNPC = animator.gameObject.GetComponent<Builder>();

        if(_builderNPC.units.Count == 0)
            _target = _home;
        else
        {
            _target = _builderNPC.units.Dequeue();
            Building.SetBuildTarget(_target);
        }
            
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float step = _builderNPC.speed * Time.deltaTime;
        _builderNPC.transform.position = Vector3.MoveTowards(_builderNPC.transform.position, _target, step);

        if(_builderNPC.transform.position == _target)
        {
            animator.SetBool("isBuilding", true);
            animator.SetBool("isWalking", false);
        }
            
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
