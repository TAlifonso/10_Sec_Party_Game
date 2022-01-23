using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpObstacle : MonoBehaviour
{
    public float speed;
    public bool Switch;
    private bool gameOver;

    public GameObject timeDisplay;

    public AudioClip loseSound;

    Rigidbody2D rigidbody2D;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Switch = true;
        rigidbody2D = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();

        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Switch)
        {
            MoveRight();
        }
        if(!Switch)
        {
            MoveLeft();
        }
        if(transform.position.x >= 2f)
        {
            Switch = false;
        }
        if(transform.position.x <= -18f)
        {
            Switch = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && gameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    void MoveRight()
    {
        transform.Translate (speed * Time.deltaTime, 0, 0);
    }
    void MoveLeft()
    {
        transform.Translate (-speed * Time.deltaTime, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            Destroy(collision.collider.gameObject);
            speed = 0;
            timeDisplay.gameObject.SetActive(false);
            PlaySound(loseSound);

            gameOver = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        speed += 1f;
    }

    void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
