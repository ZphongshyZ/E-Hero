using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MyMonobehaviour
{
    private static GameControler instance;
    public static GameControler Instance { get => instance; }

    [SerializeField] protected GameObject player;
    [SerializeField] protected GameObject bulletSpawner;

    protected override void Awake()
    {
        base.Awake();
        if (GameControler.instance != null) Debug.LogError("Only one GameControler allowed to exist");
        GameControler.instance = this;
    }


    protected override void LoadComponents()
    {
        this.player = GameObject.Find("Leo");
    }

    protected override void Start()
    {
        base.Start();
        DontDestroyOnLoad(this.player);
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(this.bulletSpawner);
    }

    public void SetPosPlayer(Vector3 pos)
    {
        this.player.transform.position = pos;
    }
}
