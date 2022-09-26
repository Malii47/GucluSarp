using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;


    public Animator animator;
    public PlayerMovement pm;
    public Rigidbody2D rb;
    public CapsuleCollider2D cd;
    public GameObject PlayerLight;
    [SerializeField] ParticleSystem playerDieParticle = null;


    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
        cd = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void SarpDie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Bullet"))
        {
            playerDieParticle.Play();
            StartCoroutine(SarpDeath());
            animator.SetTrigger("isDead");
            GameObject.Find("Legs").GetComponent<Bacak_Animation>().Anan(false);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            cd.enabled = !cd.enabled;
            pm.enabled = !pm.enabled;
            GetComponent<Combat>().enabled = false;
            GetComponentInChildren<KatanaFunction>().enabled = false;
            GameObject.FindGameObjectWithTag("Katana").SetActive(false);
            GameObject.FindGameObjectWithTag("Fade").GetComponent<LevelChanger>().FadeToNextLevel();
            GameObject.Find("Canvas").GetComponent<PauseMenu>().deactive = true;
            Invoke("SarpDie", 1f);

        }
    }
    public void DeathbySwordEnemy()
    {
        playerDieParticle.Play();
        StartCoroutine(SarpDeath());
        animator.SetTrigger("isDead");
        GameObject.Find("Legs").GetComponent<Bacak_Animation>().Anan(false);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        cd.enabled = !cd.enabled;
        pm.enabled = !pm.enabled;
        GetComponent<Combat>().enabled = false;
        GetComponentInChildren<KatanaFunction>().enabled = false;
        GameObject.FindGameObjectWithTag("Katana").SetActive(false);
        GameObject.FindGameObjectWithTag("Fade").GetComponent<LevelChanger>().FadeToNextLevel();
        GameObject.Find("Canvas").GetComponent<PauseMenu>().deactive = true;
        Invoke("SarpDie", 1f);
    }

    IEnumerator SarpDeath()
    {
        GameObject.Find("PlayerLight").GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(.25f);
        PlayerLight.SetActive(false);
    }
}