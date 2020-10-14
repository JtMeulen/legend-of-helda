using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : EnemyController
{
    [SerializeField] float timeBetweenAttacks;
    private bool canAttack = true;

    public override void StartChaseMode()
    {
        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        changeAnimDirection(temp - transform.position);
        myRigidbody.MovePosition(temp);
        ChangeState(EnemyState.walk);
        myAnimator.SetBool("aggresive", true);
    }

    public override void EndChaseMode()
    {
        myAnimator.SetBool("aggresive", false);
        ChangeState(EnemyState.idle);
    }

    public override void StartAttack()
    {
        if(canAttack)
        {
            StartCoroutine(AttackCoroutine());
        }
        else
        {
            StartChaseMode();
        }
    }

    private IEnumerator AttackCoroutine()
    {
        canAttack = false;
        ChangeState(EnemyState.attack);
        myAnimator.SetTrigger("attack");
        yield return new WaitForSeconds(.3f);
        ChangeState(EnemyState.walk);
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
    }
}
