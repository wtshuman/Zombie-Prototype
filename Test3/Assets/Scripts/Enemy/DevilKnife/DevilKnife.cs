﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DevilKnife : Enemy {
    

	void IsFeared()
	{
		this.target = GameObject.Find("Right").transform;
		this.state = CharacterState.Fleeing;
		StartCoroutine(WaitForDeath());
	}

	void IsHaunted()
	{
		if(GameObject.Find("GhostController").transform.position.x - this.transform.position.x < 0)
		{
			this.target = GameObject.Find("Right").transform;
		}
		else if(GameObject.Find("GhostController").transform.position.x - this.transform.position.x > 0)
		{
			this.target = GameObject.Find("Left").transform;
		}
		this.state = CharacterState.Fleeing;
		StartCoroutine(WaitForHauntEnd());
	}
	
	IEnumerator WaitForDeath()
	{
		yield return new WaitForSeconds(30f);
		Destroy(this.gameObject);
	}

	void TakeDamage()
	{
		this.health.CurHealth -= 20;
	}

    protected override void Attack()
    {
        if (this.distance < this.attackDistance)
        {
            this.enemyAnimator.Play("Devil_Knife_Attack");
            StartCoroutine(WaitForAnimation());
        }
        else
        {
            base.Seek(distVec);
            this.enemyAnimator.Play("Devil_Knife_Walk");
        }
    }

    protected override void Move()
    {
        this.state = CharacterState.Moving;
        if(this.distance >= this.dangerDistance + 50)
        {
            this.Seek(this.distVec);
        }

        if (this.distance < this.dangerDistance - 50)
        {
            this.Seek(this.distVec * -1);
        }

        this.enemyAnimator.Play("Devil_Knife_Walk");

        if (!this.strafing)
        {
            StartCoroutine(WaitForAttack());
        }
    }

    protected override void Stunned()
    {
        this.enemyAnimator.Play("Devil_Knife_Stunned");
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1.0f);
        this.Move();
    }

    IEnumerator WaitForAttack()
    {
        this.strafing = true;
        yield return new WaitForSeconds(this.attackRate);
        if (!this.checkForOtherAttackers())
        {
            this.state = CharacterState.Attacking;
        }
        this.strafing = false;
    }

	IEnumerator WaitForHauntEnd()
	{
		yield return new WaitForSeconds(3f);
		this.target = GameObject.Find("ZombieController").transform;
		this.state = CharacterState.Attacking;
	}

}
