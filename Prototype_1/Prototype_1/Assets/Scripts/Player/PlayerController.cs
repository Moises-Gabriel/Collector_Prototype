using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool _hitWall;
    private Camera _camera;

    void Start()
    {
        _camera = FindObjectOfType<Camera>();
    }
    void Update()
    {
        Move();
        CheckDirection();
    }

    enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }

    private Direction direction;
    void Move()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
            direction = Direction.UP;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
            direction = Direction.DOWN;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
            direction = Direction.LEFT;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
            direction = Direction.RIGHT;
        }
    }

    void CheckDirection()
    {
        if (_hitWall && (direction == Direction.UP))
        {
            StartCoroutine(Bump(new Vector3(transform.position.x, transform.position.y - 1, 0)));
        }

        if (_hitWall && (direction == Direction.DOWN))
        {
            StartCoroutine(Bump(new Vector3(transform.position.x, transform.position.y + 1, 0)));
        }

        if (_hitWall && (direction == Direction.LEFT))
        {
            StartCoroutine(Bump(new Vector3(transform.position.x + 1, transform.position.y, 0)));
        }
        if (_hitWall && (direction == Direction.RIGHT))
        {
            StartCoroutine(Bump(new Vector3(transform.position.x - 1, transform.position.y, 0)));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            _hitWall = true;
        }
    }

    IEnumerator Bump(Vector3 direction)
    {
        yield return new WaitForSeconds(0.075f);
        transform.position = direction;
        _hitWall = false;
    }
}
