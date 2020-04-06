using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kapı : MonoBehaviour
{
    [SerializeField]
    private GameObject anahtarVar;

    [SerializeField]
    private GameObject kapiAcik;

    [SerializeField]
    private AudioSource kapiKilitliSes;

    [SerializeField]
    private AudioSource kapiAcikSes;


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player" && anahtarVar.activeSelf)
        {
            kapiAcik.SetActive(true);
            kapiAcikSes.Play();
            other.gameObject.SetActive(false);
            Application.LoadLevel(1);
        }
        else if(other.gameObject.tag == "Player")
        {
            kapiKilitliSes.Play();
        }   
    }
}
