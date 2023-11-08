using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveX;
    private Animator anim;
    public float speed;
    public float jumpForce;
    public int addJump;
    public bool isGrounded;

    [Header("Config")]
    [SerializeField] Color highlightColor;
    
    // New tag for objects to be highlighted
    [SerializeField] string ContainerTag = "Container";

    List<GameObject> collidingWithList = new List<GameObject>();
    GameObject collidingWith;
    Transform item;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Interact();
        Move();

        moveX = Input.GetAxisRaw("Horizontal");
        Move();
        if (isGrounded == true)
        {
            addJump = 1;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && addJump > 0)
            {
                addJump--;
                Jump();
            }
        }
    }

    void Interact()
    {
        if (this.collidingWith == null) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            switch (this.collidingWith.tag)
            {
                case "Container":
                    ContainerController container = this.collidingWith.GetComponent<ContainerController>();

                    if (this.item != null && !container.HaveItem())
                    {
                        this.item.position = container.transform.position;
                        this.item.parent = container.transform;
                        container.SetItem(this.item);
                        this.item = null;
                        return;
                    }

                    if (this.item == null && container.HaveItem())
                    {
                        this.item = container.GetItem();
                        this.item.position = this.transform.position;
                        this.item.parent = this.transform;
                        return;
                    }
                    break;
            }
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        if (moveX > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anim.SetBool("isRun", true);
        }
        if (moveX < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anim.SetBool("isRun", true);
        }
        if (moveX == 0)
        {
            anim.SetBool("isRun", false);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetBool("isJump", true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ContainerTag))
        {
            if (!collidingWithList.Contains(collision.gameObject))
            {
                collidingWithList.Add(collision.gameObject);
                CleanHighlight();
                GetFirstCollider();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ContainerTag))
        {
            if (collidingWithList.Contains(collision.gameObject))
            {
                collidingWithList.Remove(collision.gameObject);
                CleanHighlight();
                if (collidingWithList.Count > 0)
                {
                    GetFirstCollider();
                    return;
                }
                collidingWith = null;
            }
        }
    }

    void GetFirstCollider()
    {
        if (collidingWithList.Count > 0)
        {
            collidingWith = collidingWithList[0];
            collidingWith.GetComponent<SpriteRenderer>().color = highlightColor;
        }
    }

    void CleanHighlight()
    {
        if (collidingWith != null)
        {
            SpriteRenderer spriteRenderer = collidingWith.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.white;
            }
        }
    }

    void onTriggerEnter2D (BoxCollider2D col)
    {
        Application.LoadLevel("vocevenceu");
    }
}