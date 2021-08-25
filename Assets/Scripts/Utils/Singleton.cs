using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance) return _instance;
            var objs = FindObjectOfType(typeof(T)) as T[];
            if (objs != null)
            {
                if (objs.Length > 0) _instance = objs[0];
                if (objs.Length > 1) Debug.LogError("There is more than one " + typeof(T).Name + " in scene.");
            }

            if (_instance) return _instance;
            var obj = new GameObject {hideFlags = HideFlags.HideAndDontSave};
            _instance = obj.AddComponent<T>();

            return _instance;
        }
    }
}

public class SingletonPersistent<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }

    public virtual void Awake()
    {
        if (!Instance)
        {
            Instance = this as T;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
} 