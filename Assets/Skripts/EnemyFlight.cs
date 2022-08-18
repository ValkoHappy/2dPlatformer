using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class EnemyFlight : MonoBehaviour
{
    [SerializeField] private int _duration;
    [SerializeField] private Vector2 _waypoint;
    [SerializeField] private Vector3[] _waypoints;
    [SerializeField] private UnityEvent _entered;

    private void Start()
    {
        Tween tween = transform.DOPath(_waypoints, _duration, PathType.CatmullRom, PathMode.Sidescroller2D).SetOptions(true).SetLookAt(0.01f);
        tween.SetEase(Ease.Linear).SetLoops(-1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.transform.position = _waypoint;
            _entered?.Invoke();
        }
    }
}
