﻿using UnityEngine;
using System.Collections;

public class GhostBullet : MonoBehaviour {

	private GameObject mytarget;
	public const int maxdistance = 10;
	
	void Update () 
	{

			if(this.GetComponentInChildren<Plasma>().getCurPlasma() == 0)
			{
				StartCoroutine("Recovery");
			}
			else if (Input.GetKey(KeyCode.X) && this.GetComponentInChildren<Plasma>().getCurPlasma() > 0)
			{
				this.mytarget = this.findClosestEnemy();
				this.mytarget.GetComponentInChildren<Enemy>().moveSpeed = -3;
				LineRenderer lineRenderer = this.GetComponentInChildren<LineRenderer>();
				lineRenderer.SetPosition(0, this.transform.position);
				lineRenderer.SetPosition(1, this.mytarget.transform.position);
				this.GetComponentInChildren<Plasma>().usePlasma(1);
			}

	}

	private GameObject findClosestEnemy()
	{
		GameObject[] enemies;
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

		foreach (GameObject enemy in enemies) 
		{
			Vector3 diff = enemy.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) 
			{
				closest = enemy;
				distance = curDistance;
			}
		}
		return closest;
	}

	IEnumerator Recovery ()
	{
		while(this.GetComponentInChildren<Plasma>().getCurPlasma() > this.GetComponentInChildren<Plasma>().maxPlasma)
		{	
			yield return null;
		}
	}
}
