using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Clicktomove : MonoBehaviour {

	private NavMeshAgent _navmeshagent;
	private bool _walking;
	private bool _enemyclicked;
	private Transform _targetenemy;


	private void Awake() {
		_navmeshagent = GetComponent<NavMeshAgent> ();
	}

	private void Start () {
		
	}

	private void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
        RaycastHit hit;

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
					_navmeshagent.Resume ();
				}
			}
		}
	}
}
