using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;

    private List<Vector2> _health;
    private List<Sprite> _healthSprites;
    private int _speed;

    public GameObject Player
    {
        private get;
        set;
    }
    
    private void OnEnable()
    {
        SwipeDetection.OnSwipeDirection += GetDamage;
    }

    private void OnDisable()
    {
        SwipeDetection.OnSwipeDirection -= GetDamage;
    }

    private void Start()
    {
        _health = new List<Vector2>(data.health);
        _healthSprites = new List<Sprite>(data.healthSprites);
        _speed = data.speed;
        
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _healthSprites[0];
        transform.LookAt(Player.transform);
        if(Camera.main != null) transform.GetChild(0).LookAt(Camera.main.transform);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, 
            Player.transform.position,
            _speed * Time.deltaTime);
    }

    private void GetDamage(Vector2 dir)
    {
        if (_health.Count > 0 && _health[0] == dir)
        {
            _health.RemoveAt(0);
            if (_health.Count <= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag.Equals(Player.transform.tag))
        {
            other.gameObject.GetComponent<PlayerController>().GetDamage(100);
            Destroy(gameObject);
        }
    }
}
