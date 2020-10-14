using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Turret : EnemyController
{
    [Header("Projectile")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float fireDelay;
    private float fireDelaySeconds;

    private bool canFire;

    protected override void Start()
    {
        base.Start();
        fireDelaySeconds = fireDelay;
    }

    private void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if(fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    public override void StartChaseMode()
    {
        myAnimator.SetBool("aggresive", true);
        Vector3 temp = target.transform.position - transform.position;
        changeAnimDirection(temp);
        ChangeState(EnemyState.walk);

        if (canFire)
        {
            GameObject projObj = Instantiate(projectile, transform.position, Quaternion.identity);
            projObj.GetComponent<Projectile>().Launch(temp.normalized, new Vector3(0,0,0));
            canFire = false;
        }

    }

    public override void EndChaseMode()
    {
        myAnimator.SetBool("aggresive", false);
        ChangeState(EnemyState.idle);
    }
}
