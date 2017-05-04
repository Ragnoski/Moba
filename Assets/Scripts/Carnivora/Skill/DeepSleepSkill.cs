using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepSleepSkill : PlayerSkill
{
    public float Radius;
    public LayerMask EnemyLayer;


    private Collider[] _colliders;


    public override void Damage(Player target)
    {
        base.Damage(target);
    }

    public override void Play()
    {
        base.Play();

        _colliders = Physics.OverlapSphere(transform.position, Radius, EnemyLayer);
        for (int i = 0; i < _colliders.Length; i++)
        {
            Player enemy = _colliders[i].GetComponent<Player>();
            Damage(enemy);
        }

        End();
    }

    public override void End()
    {
        base.End();
    }

    public override bool CanPlay()
    {
        return base.CanPlay();
    }
}
