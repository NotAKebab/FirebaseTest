using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PlayerBehaviour : MonoBehaviour
{
    public GameController gameController;
    public float speed = 1f;
    private Rigidbody2D rig;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rig.velocity = Vector2.up * speed;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        gameController.GameOver();
    }

}
