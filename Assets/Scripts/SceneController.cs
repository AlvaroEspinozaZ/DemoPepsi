using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject MiPanel1;
    [SerializeField] Button[] btnReload;
    [SerializeField] Button btnNextScene;
    [SerializeField] int id;
    [SerializeField] float velocityFade;
    [Header("Sonido")]
    [SerializeField] AudioSource _audioController;
    [SerializeField] AudioClip Transition;
    bool inFade;
    float velocityToFadeIn;

    private void Start()
    {
        _audioController.clip = Transition;
        _audioController.Play();
        for (int i = 0; i < btnReload.Length; i++)
        {
            btnReload[i].onClick.AddListener(delegate () { ReloadScene(); });
        }

        btnNextScene.onClick.AddListener(delegate () { ChangeScene(id); });
        velocityToFadeIn = 1;
        StartCoroutine(FadeIn());
        inFade = false;
    }
    
    public void ChangeScene(int i)
    {
        id = i;
        if (inFade)
        {
            SceneManager.LoadScene(i);
            //Debug.Log("Cambiamos");
            return;
        }
        StartCoroutine(FadeExit());

    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    IEnumerator FadeExit()
    {
        
        MiPanel1.SetActive(true);
        velocityToFadeIn = velocityToFadeIn + (velocityFade * Time.deltaTime);
        MiPanel1.GetComponent<Image>().color = new Color(0f, 0f, 0f, velocityToFadeIn);
        yield return new WaitForSecondsRealtime(0.01f);
        if (velocityToFadeIn > 1)
        {
            inFade = true;
            ChangeScene(id);
        }
        else
        {
            StartCoroutine(FadeExit());
            // Debug.Log("aver");
        }
    }

    IEnumerator FadeIn()
    {
        MiPanel1.SetActive(true);
        velocityToFadeIn = velocityToFadeIn - ((velocityFade) * Time.deltaTime);
        MiPanel1.GetComponent<Image>().color = new Color(0f, 0f, 0f, velocityToFadeIn);
        yield return new WaitForSecondsRealtime(0.01f);
        if (velocityToFadeIn < 0)
        {
            MiPanel1.SetActive(false);
            StopCoroutine(FadeIn());
        }
        else
        {
            StartCoroutine(FadeIn());
            //Debug.Log("Entrada");
        }
    }
}
