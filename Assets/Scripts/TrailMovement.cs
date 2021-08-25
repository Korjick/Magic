using System;
using System.Collections;
using UnityEngine;

public class TrailMovement : MonoBehaviour
{
    public GameObject trail;
    
    private InputManager _inputManager;
    private Coroutine _trailMove;

    private void Awake()
    {
        _inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        SwipeDetection.OnSwipeStart += PreSwipeStart;
        SwipeDetection.OnSwipeEnd += PreSwipeEnd;
    }

    private void OnDisable()
    {
        SwipeDetection.OnSwipeStart -= PreSwipeStart;
        SwipeDetection.OnSwipeEnd -= PreSwipeEnd;
    }

    private void PreSwipeStart(Vector2 position, float time)
    {
        trail.SetActive(true);
        trail.transform.localPosition = position;
        _trailMove = StartCoroutine(TrailMove());
    }

    private void PreSwipeEnd(Vector2 position, float time)
    {
        trail.SetActive(false);
        StopCoroutine(_trailMove);
    }

    private IEnumerator TrailMove()
    {
        while (true)
        {
            Vector2 primaryPosition = _inputManager.PrimaryPosition;
            trail.transform.localPosition = new Vector3(primaryPosition.x, primaryPosition.y, -1);
            yield return null;
        }
    }
}
