using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suriken_Air : MonoBehaviour
{


    public Vector2 speed = new Vector2(0.05f, 0.05f);
    public GameObject Player;
    private float rad;
    private Vector2 Position;
    public Transform player;

    void Start()
    {
//player = GetComponent<Player>();

        rad = Mathf.Atan2(
            Player.transform.position.y - transform.position.y,
            Player.transform.position.x - transform.position.x);


    }


    void Update()
    {
        Position = transform.position;

        Position.x += speed.x * Mathf.Cos(rad);
        Position.y += speed.y * Mathf.Sin(rad);

        transform.position = Position;

    }
}
