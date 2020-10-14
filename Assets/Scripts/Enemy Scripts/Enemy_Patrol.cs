using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol : EnemyController
{
    [SerializeField] Transform[] path;
    private int currentPoint;
    private Transform currentGoal;
    [SerializeField] float roundingDistance;

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
        myAnimator.SetBool("aggresive", true);
        if (currentGoal == null)
        {
            currentGoal = path[0];
        }

        if (Vector3.Distance(transform.position, currentGoal.position) > roundingDistance)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, currentGoal.position, moveSpeed * Time.deltaTime);
            changeAnimDirection(temp - transform.position);
            myRigidbody.MovePosition(temp);
        }
        else
        {
            ChangeGoal();
        }
    }

    private void ChangeGoal()
    {
        if(currentPoint == path.Length - 1)
        {
            currentPoint = 0;
        }
        else
        {
            currentPoint++;
        }
        currentGoal = path[currentPoint];
    }
}
