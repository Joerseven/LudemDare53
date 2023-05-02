using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float friction;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speed;
    [SerializeField] private Material mat;

    public Animator animator;
    private Rigidbody2D rb;
    public Collider2D TongueCollider;
    private LevelManager levelManager;
    private SpriteRenderer spriteRenderer;
    
    private static readonly int Moving = Animator.StringToHash("Moving");
    private static readonly int TongueLash = Animator.StringToHash("TongueLash");

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), TongueCollider);
        levelManager = GetComponentInParent<LevelManager>();
        
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void TurnSprite()
    {
        switch (direction.x)
        {
            case 0:
                return;
            case > 0:
                transform.localScale = new Vector3(1, 1, 1);
                break;
            default:
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
    }

    // Update is called once per frame
    void Update() {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction.Normalize();
        animator.SetBool(Moving, direction.sqrMagnitude != 0);

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger(TongueLash);
        }

        spriteRenderer.sortingOrder = (int)(transform.position.y*-100);
        
        TurnSprite();
    }

    private void FixedUpdate()
    {
        // var velocity1 = rb.velocity;
        // velocity1 += direction * (speed * Time.deltaTime);
        // rb.velocity = velocity1;
        // // rb.velocity = Vector2.MoveTowards(velocity1, Vector2.zero, 10);
        // // rb.velocity = Vector2.ClampMagnitude(velocity1, maxSpeed * Time.deltaTime);
        // print(rb.velocity);
        rb.AddForce(direction * (speed));
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    public void Damage(int damage)
    {
        Kill();
    }

    public void Kill()
    {
        levelManager.GameOver();
    }
}
