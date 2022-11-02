using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]private float _firingForce;
    [SerializeField]private float _couldown;
    [SerializeField]private bool _canFire;
    [SerializeField]private uint _ammo;
    private PlayerController _player;
    private Vector3 _firingVector;
    private GameObject _enemy;
    private Vector3 _pos;

    public virtual void Start() => _player = transform.parent.GetComponent<PlayerController>();

    private void FixedUpdate(){
        _enemy = _player._currentEnemy;
        if(_enemy == null)
            return;
        Fire();
    }   
   
    private void Fire(){
        if(_canFire){
            //Calculate velocity and fire bullet
            _pos = transform.position;
            _firingVector = transform.forward * _firingForce; 
            var bullet = ObjectPool.objectPool.GetPooledObject();
            bullet.transform.position = _pos;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().AddForce(_firingVector, ForceMode.Impulse);
            StartCoroutine("Couldown");
        }
    }

    private IEnumerator Couldown(){
        _canFire = false;
        yield return new WaitForSeconds(_couldown);
        _canFire = true;
    }
}

