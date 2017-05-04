using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [HideInInspector] public SkillController Controller;

    public Player Owner { get { return Controller.Owner; } }


    virtual public void Damage(Player target)
    {
        Controller.Damage(target);
    }

    virtual public void Play()
    {
        gameObject.SetActive(true);
        Owner.Idle(true);
    }

    virtual public void End()
    {
        Owner.Idle(false);
        Destroy(gameObject);
    }

    virtual public bool CanPlay()
    {
        return true;
    }
}
