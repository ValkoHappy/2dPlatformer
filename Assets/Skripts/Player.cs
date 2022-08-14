using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _countCoin = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out Coin coin))
        {
            _countCoin++;
        }

        if (collision.TryGetComponent<EnemyFlight>(out EnemyFlight enemyFlight))
        {
            _countCoin = 0;
        }
    }
}
