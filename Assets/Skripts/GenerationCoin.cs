using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationCoin : MonoBehaviour
{
    [SerializeField] private GameObject _template;
    [SerializeField] private Transform _point;
    [SerializeField] private float _delay;

    private string _name;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            Invoke(nameof(Generation), _delay);
        }
    }

    private void Generation()
    {
        Instantiate(_template, _point.position, Quaternion.identity);
    }

    
}

