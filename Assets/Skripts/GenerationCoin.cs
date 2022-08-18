using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationCoin : MonoBehaviour
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

    private void Spawner()
    {
        Instantiate(_template, _point.position, Quaternion.identity);
    }
}

