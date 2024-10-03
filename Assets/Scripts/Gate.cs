using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Gate : MyMonobehaviour
{
    [SerializeField] protected Transform player;
    [SerializeField] protected Vector3 nextPos;
    [SerializeField] protected string nameScene;

    protected override void LoadComponents()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void ChangeScene()
    {
        StartCoroutine(SceneController.Instance.LoadSceneTransition(this.nameScene, nextPos));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log(this.nameScene);
            this.ChangeScene();
        }
    }
}
