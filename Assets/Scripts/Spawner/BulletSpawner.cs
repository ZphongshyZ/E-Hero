using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Spawner
{
    //Singleton
    private static BulletSpawner instance;
    public static BulletSpawner Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (BulletSpawner.instance != null) Debug.LogError("Only one BulletSpawner allowed to exist");
        BulletSpawner.instance = this;
    }
}
