using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardaSeguir : MonoBehaviour
{
    public float velocidade;
    public Transform player;
    public float distanciaParaSeguir;
    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player != null)
        {
            float distancia = Vector2.Distance(transform.position, player.position);

            if (distancia < distanciaParaSeguir)
            {
                Vector2 direcao = (player.position - transform.position).normalized;
                Debug.Log(direcao);
                rb.velocity = new Vector2(velocidade * direcao.x, rb.velocity.y);

                if (transform.position.x > player.position.x)
                {
                    transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0f, 180f, 0f);
                }
                anim.SetBool("isWalk", true);
            }
            else
            {
                anim.SetBool("isWalk", false);
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene(3);
        }
    }
}
