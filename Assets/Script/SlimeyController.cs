using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlimeyController : MonoBehaviour
{

    Rigidbody2D rigidbody2D;

    public Vector2 jumpHeight;
    public bool isGrounded;

    public bool isAlive;
    private bool gameOver;

    public int WinDuration;

    public ParticleSystem slimeDroplets;

    public GameObject winText;
    public GameObject loseText;
    public Text timer;
    public GameObject timeDisplay;

    AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip gameplaySound;
    public AudioClip winSound;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        Countdown();
        WinDuration = 10;

        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);

        isAlive = true;

        audioSource = GetComponent<AudioSource>();

        gameOver = false;

    }

    IEnumerator CountdownToWin()
    {
        while (WinDuration >= 0)
        {

            timer.text = WinDuration.ToString();
            yield return new WaitForSeconds(1f);

            WinDuration--;
        }
        if (WinDuration == 0)
        {
            StopCoroutine(CountdownToWin());
        }
        
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(jumpHeight, ForceMode2D.Impulse);
            PlaySound(jumpSound);
        }

        if (Input.GetKeyDown(KeyCode.R) && gameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (WinDuration == 0 && isAlive)
        {
            GameObject.Find("Crabby").GetComponent<JumpObstacle>().speed = 0;
            GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Stop();
            PlayWinSound();
            
            timeDisplay.gameObject.SetActive(false);
            winText.gameObject.SetActive(true);

            gameOver = true;

        }
    }

    void Countdown()
    {
        StartCoroutine(CountdownToWin());
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            isGrounded = true;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            isGrounded = false;
        }    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Crab")
        {
            loseText.gameObject.SetActive(true);
            GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Stop();
            audioSource.Pause();
        }

        if (collision.collider.tag == "Floor")
        {
            slimeDroplets.Play();
        }
    }

    void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    void PlayWinSound()
    {
        audioSource.volume = 0.1f;
        audioSource.PlayOneShot(winSound);
    }
}
