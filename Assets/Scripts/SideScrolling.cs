using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrolling : MonoBehaviour
{

    private Transform player;

    public float height = 7f;
    public float underGroundHeight = -8.5f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        transform.position = cameraPosition;
    }

    public void SetUnderGround(bool underGround)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underGround ? underGroundHeight : height;
        transform.position = cameraPosition;
    }

}
