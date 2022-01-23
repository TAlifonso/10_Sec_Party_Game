using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextKillCode : MonoBehaviour
{
    public GameObject timeDisplay;

    // Start is called before the first frame update
    void Start()
    {
        timeDisplay.gameObject.SetActive(false);
        Time.timeScale = 0;
        AudioListener.pause = true;   
        StartCoroutine(Unfreeze());
    }

    IEnumerator Unfreeze()
    {
        while(Time.timeScale == 0)
        {
            yield return new WaitForSecondsRealtime(2f);

            Time.timeScale = 1;
            timeDisplay.gameObject.SetActive(true);
            AudioListener.pause = false;
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
