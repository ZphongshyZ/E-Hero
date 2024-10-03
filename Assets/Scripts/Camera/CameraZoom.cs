using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MyMonobehaviour
{
    [SerializeField] protected CinemachineVirtualCamera virtualCamera;
    [SerializeField] protected float zoomFOV;
    [SerializeField] protected float zoomSpeed = 1f;

    [SerializeField] protected float originalFOV;
    protected override void LoadComponents()
    {
        this.virtualCamera = GetComponent<CinemachineVirtualCamera>();
        this.originalFOV = virtualCamera.m_Lens.OrthographicSize;
        this.zoomFOV = this.originalFOV;
    }

    protected void Update()
    {
        this.Zooming();
    }

    protected virtual void Zooming()
    {
        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, zoomFOV, zoomSpeed * Time.deltaTime);
    }

    public void SetZoomFOV(float fov)
    {
        this.zoomFOV = fov;
    }

    public float GetZoomFOV()
    {
        return zoomFOV;
    }
}
