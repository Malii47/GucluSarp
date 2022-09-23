using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public bool pauseMenu = false;
    public GameObject pauseMenuUI;
    public Animator animator;

    private int mainMenu;
    GameObject a;

    void OnEnable()
    {
        pauseMenu = false;
    }

    void Start()
    {
        pauseMenu = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu = true;
            if (pauseMenu == true)
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }           
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseMenu = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().enabled = true;
        GameObject.FindGameObjectWithTag("Katana").GetComponent<KatanaFunction>().enabled = true;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        pauseMenu = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().enabled = false;
        GameObject.FindGameObjectWithTag("Katana").GetComponent<KatanaFunction>().enabled = false;
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        StartCoroutine(ExitGame());
    }

    public IEnumerator ExitGame()
    {
        a = GameObject.Find("PauseMenu");
        a.SetActive(false);
        animator = GameObject.Find("LevelChanger").GetComponent<LevelChanger>().animator;
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Menu");
    }
}
