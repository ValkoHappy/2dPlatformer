using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class PickUpCoin : MonoBehaviour
{
    [SerializeField] private float _delay;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void RemoveCoin()
    {
        _spriteRenderer.DOFade(0, 1);
        transform.DOMoveY(5, 2);
        Invoke(nameof(Remove), _delay);
    }

    private void Remove()
    {
        Destroy(gameObject);
    }
}
