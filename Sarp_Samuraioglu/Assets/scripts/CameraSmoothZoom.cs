using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSmoothZoom : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera vcam;

    public float speed;

    private void Start()
    {
        vcam = vcam.GetComponent<CinemachineVirtualCamera>();
    }

    public void Update() //LateUpdate
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().camZoom == true && GameObject.Find("PauseMenuManager").GetComponent<PauseMenu>().pauseMenu == false)
        {
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, 3, speed);
            Time.timeScale = 0.5f;
        }
        else if(GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().camZoom == false && GameObject.Find("PauseMenuManager").GetComponent<PauseMenu>().pauseMenu == false)
        {
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, 5, speed);
            Time.timeScale = 1f;
        }   
    }
}
