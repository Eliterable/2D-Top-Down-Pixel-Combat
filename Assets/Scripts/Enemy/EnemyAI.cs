using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float waitBeforeMove = 2f;
    [SerializeField] float attackRange = 5f;
    [SerializeField] private MonoBehaviour enemyType;
    private bool canAttack = true;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMoveingWhileAttacking = false;

    private enum State                                              //enum to specjalny typ, który pozwala zdefiniować własny zbiór stanów.
    {
        Roaming,
        Attacking
    }

    private State state;                                 //state to zmienna, która przechowuje aktualny stan obiektu. Jest typu State (czyli tego, co zdefiniowałeś wcześniej jako enum)
    private EnemyPathfinding enemyPathfinding;
    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    private void Awake()
    {
        state = State.Roaming;
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }
    private void Start()
    {
        roamPosition = GetRoamingPosition();
        GetRoamingPosition();
    }
    private void Update()
    {
        MovementStateControl();
    }
    private void MovementStateControl()
    {
        switch (state)
        {

            default:
            case State.Roaming:
                Roaming();
                break;

            case State.Attacking:
                Attacking();
                break;
        }
    }
    private void Roaming()
    {
        timeRoaming += Time.deltaTime;
        enemyPathfinding.MoveTo(roamPosition);
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }
        if (timeRoaming > waitBeforeMove)
        {
            roamPosition = GetRoamingPosition();
        }
    }
    private void Attacking()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
        }
        if (attackRange != 0 && canAttack)
        {
            canAttack = false;
            (enemyType as IEnemy).Attack();
            if (stopMoveingWhileAttacking)
            {
                enemyPathfinding.StopMoving();
            }
            else
            {
                enemyPathfinding.MoveTo(roamPosition);
            }
            StartCoroutine(AttackCooldownRoutine());
        }
    }
    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;

    }


    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }








}
