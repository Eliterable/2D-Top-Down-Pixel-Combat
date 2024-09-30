using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] float movespeed = 1f;
    [SerializeField] float dashSpeed = 4f;
    [SerializeField] TrailRenderer dashTrailRenderer;
    [SerializeField] private Transform weaponCollider;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer sr;
    private bool faceingLeft = false;
    private bool isDashing = true;
    private float startingMoveSpeed;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();


    }
    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = movespeed;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();

    }

    private void FixedUpdate()
    {
        Move();
        PlayerFaceingDirection();
    }
    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }


    void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);


    }

    void Move()
    {
        rb.MovePosition(rb.position + movement * (movespeed * Time.fixedDeltaTime));
    }

    void PlayerFaceingDirection()
    {
        Vector3 pozycjaMyszki = Input.mousePosition;
        Vector3 pozycjaGracza = Camera.main.WorldToScreenPoint(transform.position);
        if (pozycjaMyszki.x < pozycjaGracza.x)
        {
            sr.flipX = true;
            faceingLeft = true;
        }
        else
        {
            sr.flipX = false;
            faceingLeft = false;
        }
    }
    public bool FaceingLeft
    {
        get
        {
            return faceingLeft;
        }

    }

    private void Dash()
    {
        if (isDashing)
        {
            isDashing = false;
            StartCoroutine(Dashing());
        }

    }
    private IEnumerator Dashing()
    {
        float dashTime = 0.2f;
        float dashCD = 0.5f;
        movespeed *= dashSpeed;
        dashTrailRenderer.emitting = true;
        yield return new WaitForSeconds(dashTime);
        movespeed = startingMoveSpeed;
        dashTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = true;

    }


























}
