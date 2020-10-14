using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    [SerializeField] private Door[] doors;
    private int enemyCount;

    protected override void Start()
    {
        base.Start();
        enemyCount = enemies.Length;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            base.OnTriggerEnter2D(other);
            enemyCount = enemies.Length;
        }
    }

    public void CheckEnemies()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            OpenDoors();
        }
        else
        {
            CloseDoors();
        }
    }

    private void CloseDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].CloseDoor();
        }
    }

    private void OpenDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].OpenDoor();
        }
    }
}
