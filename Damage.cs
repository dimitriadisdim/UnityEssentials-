using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

public class Damage : MonoBehaviour
{
    [SerializeField] private int life;

    private void DamageObject(GameObject obj)
    {
        if (obj.GetComponent<IDamageble>() == null)
            return;
        obj.GetComponent<IDamageble>().Damage(life);
        
        if(obj.GetComponent<Rigidbody2D>() == null)
            return;
        var push = (transform.position - obj.transform.position);
        obj.GetComponent<Rigidbody2D>().AddForce(push,ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col) => DamageObject(col.gameObject);
}
