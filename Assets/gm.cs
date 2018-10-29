using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gm : MonoBehaviour {

    public TextMeshProUGUI timerText;

    public AudioClip start;
    public AudioClip end;

    AudioSource audioSource;

    public int waitingPeriod;
    int countdownTime = 4;
    public int roundTime;
    public int roundEndDelay;

    public bool finale;

    Coroutine currentPeriod;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentPeriod = StartCoroutine(runde());
    }

    public void Finale ()
    {
        timerText.text = "Gør klar til FINALEN!";
        StopCoroutine(currentPeriod);
        finale = true;
    }

    public void startFinale()
    {
        if (finale)
        {
            currentPeriod = StartCoroutine(finaleRoutine());
        }
    }

    public void stopFinale()
    {
        finale = false;
        StopCoroutine(currentPeriod);
        currentPeriod = StartCoroutine(runde());
    }

    IEnumerator finaleRoutine()
    {
        for (int i = 10; i != 0; i--)
        {
            timerText.text = "Finalen starter om: \n" + Mathf.Floor(i / 60).ToString("F0") + ":" + Mathf.Floor(i % 60).ToString("00");
            if (i == countdownTime)
                audioSource.PlayOneShot(start);
            yield return new WaitForSeconds(1);
        }

        for (int i = roundTime; i != -1; i--)
        {
            timerText.text = "Tid Tilbage: \n" + Mathf.Floor(i / 60).ToString("F0") + ":" + Mathf.Floor(i % 60).ToString("00");
            yield return new WaitForSeconds(1);
        }

        audioSource.PlayOneShot(end);

        yield return new WaitForSeconds(5);
    }

    IEnumerator runde ()
    {
        for (int i = waitingPeriod; i != 0; i--)
        {
            timerText.text = "Spillet starter om: \n" + Mathf.Floor(i / 60).ToString("F0") + ":" + Mathf.Floor(i % 60).ToString("00");
            if (i == countdownTime)
                audioSource.PlayOneShot(start);
            yield return new WaitForSeconds(1);
        }

        for (int i = roundTime; i != -1; i--)
        {
            timerText.text = "Tid Tilbage: \n" + Mathf.Floor(i / 60).ToString("F0") + ":" + Mathf.Floor(i % 60).ToString("00");
            yield return new WaitForSeconds(1);
        }

        audioSource.PlayOneShot(end);

        yield return new WaitForSeconds(roundEndDelay);
        currentPeriod = StartCoroutine(runde());
    }
}
