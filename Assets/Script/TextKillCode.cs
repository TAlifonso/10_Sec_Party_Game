using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextKillCode : MonoBehaviour
{
    public GameObject timeDisplay;

    public AudioClip announce;

    // Start is called before the first frame update
    void Start()
    {

        timeDisplay.gameObject.SetActive(false);
        Time.timeScale = 0;
        GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Stop();
        StartCoroutine(Unfreeze());
    }

    IEnumerator Unfreeze()
    {
        while(Time.timeScale == 0)
        {
            yield return new WaitForSecondsRealtime(2f);

            Time.timeScale = 1;
            timeDisplay.gameObject.SetActive(true);
            GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Play();
        }
        if (Time.timeScale == 1)
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
