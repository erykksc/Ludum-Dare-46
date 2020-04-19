using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging : Character
{
    [Header("damage - Damaging class property")]
    [SerializeField] public int damage;

    public void CheckAndDamage(GameObject collisionGameObject)
    {
        if (collisionGameObject.tag == "Player" || collisionGameObject.tag == "Kid")
        {
            collisionGameObject.GetComponent<Character>().dealDamage(damage);
            GameObject.Find("LevelManager").GetComponentInChildren<UI_Handler>().SetHealthBar(collisionGameObject.gameObject.GetComponent<Character>().HP);
            Debug.Log("damaging" + collisionGameObject.gameObject.GetComponent<Character>().HP);
        }
    }
    public void KnockBack()
    {

    }


}
