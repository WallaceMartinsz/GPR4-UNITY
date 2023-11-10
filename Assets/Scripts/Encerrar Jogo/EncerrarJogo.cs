using UnityEngine;
using UnityEngine.SceneManagement;

public class EncerrarJogo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // verifica se a tecla 'ESC' foi apertada
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // roda o método 'SairDoJogo()'
            SairDoJogo();
        }
    }

    public void SairDoJogo()
    {
        // escreve uma mensagem na aba 'Console' para termos certeza de que esse método foi chamado
        Debug.Log("Saiu do jogo");
        // fecha o nosso jogo
        Application.Quit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }
}