using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sleep : EnemyController
{
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
}
