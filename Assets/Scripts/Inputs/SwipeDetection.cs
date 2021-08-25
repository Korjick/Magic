using UnityEngine;
using UnityEngine.Events;

public class SwipeDetection : MonoBehaviour
{
    public static UnityAction<Vector2, float> OnSwipeStart;
    public static UnityAction<Vector2, float> OnSwipeEnd;
    public static UnityAction<Vector2> OnSwipeDirection;

    private InputManager _inputManager;
    private Vector2 _startPosition, _endPosition;
    private float _startTime, _endTime;

    public float minimumDistance = .2f;
    public float maximumTime = 1f;
    [Range(0, 1)] public float directionThreshold = .9f;

    private void Awake()
    {
        _inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        _inputManager.OnStartTouch += SwipeStart;
        _inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        _inputManager.OnStartTouch -= SwipeStart;
        _inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        OnSwipeStart?.Invoke(position, time);
        _startPosition = position;
        _startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        OnSwipeEnd?.Invoke(position, time);
        _endPosition = position;
        _endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(_startPosition, _endPosition) >= minimumDistance
            && (_endTime - _startTime) <= maximumTime)
        {
            Vector3 direction = _endPosition - _startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
            OnSwipeDirection?.Invoke(Vector2.up);
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
            OnSwipeDirection?.Invoke(Vector2.down);
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
            OnSwipeDirection?.Invoke(Vector2.left);
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
            OnSwipeDirection?.Invoke(Vector2.right);
        else if (Vector2.Dot(Vector2.one, direction) > directionThreshold)
            OnSwipeDirection?.Invoke(Vector2.one);
        else if (Vector2.Dot(Vector2.one * -1, direction) > directionThreshold)
            OnSwipeDirection?.Invoke(Vector2.one * -1);
        else if (Vector2.Dot(new Vector2(1, -1),direction) > directionThreshold)
            OnSwipeDirection?.Invoke(new Vector2(1, -1));
        else if (Vector2.Dot(new Vector2(-1, 1),direction) > directionThreshold)
            OnSwipeDirection?.Invoke(new Vector2(-1, 1));
    }
}
