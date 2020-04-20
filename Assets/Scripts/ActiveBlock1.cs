using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBlock1 : Activatable
{
    // Start is called before the first frame update
    SpriteRenderer rende;
    Collider2D colli;
    [SerializeField] float time = 1;
    void Start()
    {
        rende = GetComponent<SpriteRenderer>();
        colli = GetComponent<Collider2D>();
    }
    IEnumerator Fade()
    {
        for(int i = 0;i<10;i++)
        {
            rende.color = new Color(1f,1f,1f,(9-i)*1f/9f);
            yield return new WaitForSeconds(time/9f);
        }
        colli.enabled = false;
        rende.enabled = false;
        yield return null;
    }
    IEnumerator Unfade()
    {
        colli.enabled = true;
        rende.enabled = true;
        for(int i = 0;i<10;i++)
        {
            rende.color = new Color(1f,1f,1f,i*1f/9f);
            yield return new WaitForSeconds(time/9f);
        }
        yield return null;
    }
    override public void activate()
    {
        StartCoroutine(Fade());
    }
    // Update is called once per frame
    override public void de_activate()
    {
        StartCoroutine(Unfade());
    }
}
