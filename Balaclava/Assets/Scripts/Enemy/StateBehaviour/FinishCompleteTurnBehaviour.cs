using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCompleteTurnBehaviour : EnemyStateBehaviour
{
    private static int repetitions = 0;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        contador = 0;
        called = false;
        ec = animator.gameObject.GetComponent<EnemyController>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        float time = animatorStateInfo.normalizedTime % 1;

        if (time >= 0.92 && !called && repetitions == 1)
        {
            called = true;
            AnimatorClipInfo[] m_CurrentClipInfo = animator.GetCurrentAnimatorClipInfo(layerIndex);
            //Debug.Log("COP: " + ec.gameObject.name + " ,ONSTATEUPDATE CALL FinishTurnBehaviour, ANIMATION name= " + m_CurrentClipInfo[0].clip.name);
            repetitions = 0;
            ec.endTurn();
        }
        else if (time >= 0.92 && repetitions == 0 && !called)
        {
            repetitions++;
            //Debug.Log("Actualizo REPETITIONS = " + repetitions);
            called = true;
            ec.executeTotalTurn();          
        }       
    }
}
