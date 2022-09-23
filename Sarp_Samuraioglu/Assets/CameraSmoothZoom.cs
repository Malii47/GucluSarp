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

    public void LateUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().x == 1f)
        {
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, 3, speed);
            Time.timeScale = 0.5f;
        }
        else
        {
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, 5, speed);
            StartCoroutine("Zoom");
        }   
    }

    IEnumerator Zoom()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1f;
        yield return null;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().x = 0f;
        yield return null;
    }
}
