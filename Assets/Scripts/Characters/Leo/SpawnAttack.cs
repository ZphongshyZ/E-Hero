using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAttack : MyMonobehaviour
{
    [SerializeField] protected LayerMask ground;
    [SerializeField] protected float distance = 100f;

    private void Update()
    {
        this.SetPosGround();
    }

    protected float GetDistanceGround()
    {
        // Vị trí hiện tại của đối tượng
        Vector2 position = this.transform.position;

        // Phóng tia ray xuống dưới theo trục y (Vector2.down)
        RaycastHit2D hitGround = Physics2D.Raycast(position, Vector2.down, this.distance, this.ground);

        // Kiểm tra xem tia ray có chạm vào "Ground" không
        if (hitGround.collider != null)
        {
            // Tính khoảng cách từ đối tượng đến tilemap "Ground"
            return hitGround.distance;
        }
        else
        {
            return 0f;
        }
    }

    protected void SetPosGround()
    {
        float disGround = GetDistanceGround();
        if (Mathf.Abs(disGround - 5.5f) > 0.01f)
        {
            float adjustment = disGround - 5.5f;
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - adjustment);
        }
    }
}
