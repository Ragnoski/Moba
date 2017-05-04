using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Clicktomove : MonoBehaviour {

	private bool _walking;
	private bool _enemyclicked;
	private Transform _targetenemy;
    private Vector3 _destination;
    private bool _canmove;


    private void Awake() {
	}

	private void Start() {
        _canmove = true;
	}

	private void Update() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
        RaycastHit hit;

        if (_canmove)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        return;
                    }

                    if (hit.collider.CompareTag("Enemy"))
                    {
                        _targetenemy = hit.transform;
                        _enemyclicked = true;
                    }
                    else
                    {
                        _enemyclicked = false;
                        _destination = hit.point;

                        this.transform.LookAt(new Vector3(_destination.x, this.transform.position.y, _destination.z));
                    }

                    _walking = true;
                }
            }
        }
	}

    private void FixedUpdate()
    {
        if (_walking)
        {
            this.transform.position += this.transform.forward * 2f * Time.deltaTime;
            if (Vector3.Distance(this.transform.position, _destination) <= 0.1f)
            {
                _walking = false;
            }
        }
    }


    //public bool ReachedDestination()
    //{
    //    if(!_navmeshagent.pathPending)
    //    {
    //        if( _navmeshagent.remainingDistance <= _navmeshagent.stoppingDistance)
    //        {
    //            if(!_navmeshagent.hasPath || _navmeshagent.velocity.sqrMagnitude == 0f)
    //            {
    //                return true;
    //            }
    //        }
    //    }

    //    return false;
    //}

    public void Enable(bool value) {
        _canmove = value;
        _targetenemy = null;
        _enemyclicked = false;
        _destination = Vector3.zero;
        _walking = false;
    }
}
