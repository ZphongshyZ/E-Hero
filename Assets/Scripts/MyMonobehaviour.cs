using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMonobehaviour : MonoBehaviour
{
    protected virtual void Start()
    {

    }

    protected virtual void Reset()
    {
        this.LoadComponents();
        this.ResetValue();
    }

    protected virtual void Awake()
    {
        this.LoadComponents();
        this.ResetValue();
    }

    protected virtual void LoadComponents()
    {
        //For override
    }

    protected virtual void ResetValue()
    {
        //For override
    }

    protected virtual void OnEnable()
    {
        //For ovverride
    }
}
