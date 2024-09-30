using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float waitBeforeMove = 2f;



    private enum State                                              //enum to specjalny typ, który pozwala zdefiniować własny zbiór stanów.
    {
        Roaming
    }

    private State state;                                 //state to zmienna, która przechowuje aktualny stan obiektu. Jest typu State (czyli tego, co zdefiniowałeś wcześniej jako enum)
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        state = State.Roaming;
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }
    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(waitBeforeMove);


        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }








}
