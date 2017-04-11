using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceSkill : PlayerSkill
{
    public float Radius;
    public LayerMask EnemyLayer;


    private Collider[] _colliders;


    private Collider[] GetColliders()
    {
        return Physics.OverlapSphere(transform.position, Radius, EnemyLayer);
    }


    public override void Damage(Player target)
    {
        base.Damage(target);
    }

    public override void Play()
    {
        base.Play();

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
        _colliders = GetColliders();
        if (_colliders.Length == 0)
            return false;

        return base.CanPlay();
    }
}
