using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Clicktomove : MonoBehaviour {

    [HideInInspector] bool CanMove;
    [HideInInspector] bool StartMove;

	private NavMeshAgent _navmeshagent;
	private bool _walking;
	private bool _enemyclicked;
	private Transform _targetenemy;


	private void Awake() {
		_navmeshagent = GetComponent<NavMeshAgent> ();
	}

	private void Start() {
        CanMove = true;
	}

	private void Update() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
        RaycastHit hit;

        if (CanMove)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        _targetenemy = hit.transform;
                        _enemyclicked = true;
                    }
                    else
                    {
                        _walking = true;
                        _enemyclicked = false;
                        _navmeshagent.destination = hit.point;
                    }

                    StartMove = true;
                }
            }

            if (StartMove)
            {
                _navmeshagent.Resume();
            }
        }
	}


    public bool ReachedDestination()
    {
        if(!_navmeshagent.pathPending)
        {
            if( _navmeshagent.remainingDistance <= _navmeshagent.stoppingDistance)
            {
                if(!_navmeshagent.hasPath || _navmeshagent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void Enable(bool value) {
        CanMove = value;

        if (!value)
        {
            _navmeshagent.Stop();
            _navmeshagent.destination = Vector3.zero;
        }
    }
}
