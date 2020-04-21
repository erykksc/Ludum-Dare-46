using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Co robi:
Utrzymuje kamerę w miejscu jeżeli gracz nie przejdzie pewnej granicy.

ponadto:
przy przejściu do danego poziomu, ustawia pozycję gracza.

Na czym powinien być:
Na kamerze

Jakich komponentów wymaga: (np. Rigidbody2D)
Transforma i playera w scenie

Specjalne ustawienia objektu:
Brak
*/

public class CameraMovement : MonoBehaviour
{
    [SerializeField] public Transform followed;
    //Rozmiar pola w którym kamera stoi w miejscu
    [SerializeField] int size;
    //Jak szybko kamera dogania gracza
    [SerializeField] int tightness;


    [SerializeField] Vector2 spawn_coordinates;

    void Start()
    {
        int count = Resources.FindObjectsOfTypeAll<Player>().Length;
        if(count>0)
        {
            Player player = Resources.FindObjectsOfTypeAll<Player>()[0];
            followed = player.transform;
        }
    }
    void Update()
    {
        if(followed==null)
        {
            int count = Resources.FindObjectsOfTypeAll<Player>().Length;
            if(count>0)
            {
                Player player = Resources.FindObjectsOfTypeAll<Player>()[0];
                followed = player.transform;
            }
            return;
        }
        Vector3 dir = followed.position-transform.position;
        dir.z = 0;
        dir.y = dir.z = 0;
        if(dir.magnitude>size)
        {
            dir = (transform.position)*(1-Time.fixedDeltaTime*tightness) + (followed.position-dir.normalized*size)*Time.fixedDeltaTime*tightness;
            dir.z = transform.position.z;
            transform.position = dir;
        }
        else
        {
            dir = (transform.position)*(1-Time.fixedDeltaTime*tightness) + (followed.position-dir.normalized*size)*Time.fixedDeltaTime*tightness;
            dir.z = transform.position.z;
            dir.x = transform.position.x;
            transform.position = dir;
        }
    }
}
