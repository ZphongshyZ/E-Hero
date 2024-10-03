using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSpawnByDis : DeSpawner
{
    //Properties
    [SerializeField] protected float dislimit = 35f;
    [SerializeField] protected float distance = 0f;

    //GameObj
    [SerializeField] protected Transform mainCam;

    //LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCamera();
    }

    protected virtual void LoadCamera()
    {
        if (this.mainCam != null) return;
        this.mainCam = Transform.FindAnyObjectByType<Camera>().transform;
    }

    //DeSpawn
    protected override bool CanDeSpawn()
    {
        this.distance = Vector3.Distance(transform.position, this.mainCam.position);
        if (this.distance > this.dislimit) return true;
        return false;
    }
}
