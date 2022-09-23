using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcam;
    //[SerializeField] float sensitivity = 10f;

    CinemachineComponentBase componentBase;
    float cameraDistance;



    /*public float initialFOV;
    public float zoomInFOV;
    public float smooth;
    private float currentFOV;
    */
    void Update()
    {
        //GameObject.Find("Enemy_Sword").GetComponent<EnemyStun>().Stun()

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().x == 1)
        {
            //vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_MaximumOrthoSize = 3;

            StartCoroutine("Zoom");
        }
    }

    IEnumerator Zoom()
    {
        yield return null;
        vcam.m_Lens.OrthographicSize = 4f;
        yield return new WaitForSeconds(1.5f);
        vcam.m_Lens.OrthographicSize = 5f;

        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().x = 0;
        yield return null;
    }


}
