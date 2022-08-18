using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Coin _template;
    [SerializeField] private Transform _point;
    [SerializeField] private float _delay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            Invoke(nameof(Spawner), _delay);
        }
    }

    private void Spawn()
    {
        Instantiate(_template, _point.position, Quaternion.identity);
    }
}

