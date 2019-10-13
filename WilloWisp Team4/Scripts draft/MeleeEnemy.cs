using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Jester
public class MeleeEnemy : Enemy
{
    public float stopDistance;

    private float attackTimer;

    public float attackSpeed;

    private void Update()
    {

        if (player != null)
        {

            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {

                if (Time.time >= attackTimer)
                {
                    attackTimer = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }

            }

        }

    }


    IEnumerator Attack()
    {

        player.GetComponent<Player>().TakeDamage(damage);// this deal "X" amount of damage 2 the Player

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0f;
        while (percent <= 1)
        {

            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, interpolation);
            yield return null;

        }

    }
}