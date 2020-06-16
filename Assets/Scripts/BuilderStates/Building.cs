using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : StateMachineBehaviour
{
    Builder _builderNPC;
    Vector3 _target;
    float _buildingTime;
    float _startTime;
    PlaceUnitController puc;

    private void OnEnable()
    {
        puc = GameObject.FindWithTag("Placer").GetComponent<PlaceUnitController>();
    }

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _builderNPC = animator.gameObject.GetComponent<Builder>();
        _buildingTime = _builderNPC.buildingTime;
        _startTime = Time.deltaTime;

        if(_builderNPC.units.Count == 0)
            _target = _builderNPC._home.transform.position;
        else
        {
            _target = _builderNPC.units.Dequeue();
        }
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _startTime += Time.deltaTime;
        if(_startTime >= _buildingTime)
        {
            animator.SetBool("isBuilding", false);
            animator.SetBool("isWalking", true);
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        puc.PlaceObject(_target);
    }


}
