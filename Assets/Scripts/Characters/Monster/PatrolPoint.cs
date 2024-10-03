using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MyMonobehaviour
{
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(this.transform.position, 0.5f);
    }
}
