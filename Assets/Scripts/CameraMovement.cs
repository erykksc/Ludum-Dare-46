using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Co robi:
Utrzymuje kamerę w miejscu jeżeli gracz nie przejdzie pewnej granicy.

Na czym powinien być:
Graczu/ Player

Jakich komponentów wymaga: (np. Rigidbody2D)
Transform

Specjalne ustawienia objektu:
Brak
*/

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform followed = null;
    //Rozmiar pola w którym kamera stoi w miejscu
    [SerializeField] int size = 3;
    //Jak szybko kamera dogania gracza
    [SerializeField] int tightness = 10;

    void Start()
    {
        int count = Resources.FindObjectsOfTypeAll<Player>().Length;
        if(count>0)
        {
            Debug.Log("Setting up player");
            Player player = Resources.FindObjectsOfTypeAll<Player>()[0];
            player.transform.position = new Vector3(0,0,0);
            player.gameObject.SetActive(true);
            followed = player.transform;
        }
        count = Resources.FindObjectsOfTypeAll<Kid>().Length;
        if(count>0)
        {
            Debug.Log("Setting up kid");
            Kid kid = Resources.FindObjectsOfTypeAll<Kid>()[0];
            kid.transform.position = new Vector3(0,1,0);
            kid.gameObject.SetActive(true);
        }
    }
    void Update()
    {
        if(followed==null){return;}
        Vector3 dir = followed.position-transform.position;
        dir.z = dir.y = 0;
        //dir.y = dir.z = 0;
        if(dir.magnitude>size)
        {
            dir = (transform.position)*(1-Time.fixedDeltaTime*tightness) + (followed.position-dir.normalized*size)*Time.fixedDeltaTime*tightness;
            dir.z = transform.position.z;
            dir.y = transform.position.y;
            transform.position = dir;
        }
    }
}
