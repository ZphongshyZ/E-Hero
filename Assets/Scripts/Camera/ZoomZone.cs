using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomZone : MyMonobehaviour
{
    [SerializeField] protected float zoomFOV;
    public float ZoomFOV { get { return zoomFOV; } }

    [SerializeField] protected float originalFOV;

    [SerializeField] protected CameraZoom cameraZoom;

    protected override void Start()
    {
        this.originalFOV = this.cameraZoom.GetZoomFOV();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.GetComponent<PlayerMovement>() != null)
        {
            this.cameraZoom.SetZoomFOV(zoomFOV);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<PlayerMovement>() != null)
        {
            this.cameraZoom.SetZoomFOV(this.originalFOV);
        }
    }
}
