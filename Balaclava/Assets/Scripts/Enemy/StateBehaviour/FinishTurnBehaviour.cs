using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTurnBehaviour : EnemyStateBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        called = false;
        ec = animator.gameObject.GetComponent<EnemyController>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        float time = animatorStateInfo.normalizedTime % 1;
        if (time >= 0.92 && !called)
        {
            called = true;
            AnimatorClipInfo[] m_CurrentClipInfo = animator.GetCurrentAnimatorClipInfo(layerIndex);
            //Debug.Log("COP: " + ec.gameObject.name +  " ,ONSTATEUPDATE CALL FinishTurnBehaviour, ANIMATION name= " + m_CurrentClipInfo[0].clip.name);            
            ec.endTurn();
        }
    }
}
