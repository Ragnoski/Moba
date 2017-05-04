using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushItSkill : PlayerSkill
{
    public float Distance;
    public float Speed;


    private bool _startmovement;
    private Vector3 _endpoint;


    private void FixedUpdate()
    {
        if(_startmovement)
        {
            base.Owner.transform.position += base.Owner.transform.forward * Speed * Time.deltaTime;
            if (Vector3.Distance(base.Owner.transform.position, _endpoint) <= 0.1f)
            {
                End();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Player enemy = other.GetComponent<Player>();
            Damage(enemy);
            End();
        }
    }


    public override void Damage(Player target)
    {
        base.Damage(target);
    }

    public override void Play()
    {
        base.Play();
        _endpoint = base.Owner.transform.position + base.Owner.transform.forward * Distance;
        _startmovement = true;
    }

    public override void End()
    {
        base.End();
        _startmovement = false;
    }

    public override bool CanPlay()
    {
        return base.CanPlay();
    }
}
