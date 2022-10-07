using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private bool _hitWall;
    private Camera _camera;
    internal AudioSource _audioSource;
    public AudioClip Dig;
    public AudioClip[] Footsteps;

    void Start()
    {
        _camera = FindObjectOfType<Camera>();
        _audioSource = GetComponent<AudioSource>();
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
            _audioSource.PlayOneShot(StepSound());
            direction = Direction.UP;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
            _audioSource.PlayOneShot(StepSound());
            direction = Direction.DOWN;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
            _audioSource.PlayOneShot(StepSound());
            direction = Direction.LEFT;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
            _audioSource.PlayOneShot(StepSound());
            direction = Direction.RIGHT;
        }
    }

    AudioClip StepSound()
    {
        AudioClip steps = Footsteps[Random.Range(0, Footsteps.Length)];
        return steps;
    }

    void CheckDirection()
    {
        if (_hitWall && (direction == Direction.UP))
        {
            StartCoroutine(Bump(new Vector3(transform.position.x, transform.position.y - 1, 0)));
            _audioSource.PlayOneShot(Dig);
        }

        if (_hitWall && (direction == Direction.DOWN))
        {
            StartCoroutine(Bump(new Vector3(transform.position.x, transform.position.y + 1, 0)));
            _audioSource.PlayOneShot(Dig);
        }

        if (_hitWall && (direction == Direction.LEFT))
        {
            StartCoroutine(Bump(new Vector3(transform.position.x + 1, transform.position.y, 0)));
            _audioSource.PlayOneShot(Dig);
        }
        if (_hitWall && (direction == Direction.RIGHT))
        {
            StartCoroutine(Bump(new Vector3(transform.position.x - 1, transform.position.y, 0)));
            _audioSource.PlayOneShot(Dig);
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
        if (_audioSource.isPlaying)
            _audioSource.Stop();
        yield return new WaitForSeconds(0.075f);
        transform.position = direction;
        _hitWall = false;
    }
}
