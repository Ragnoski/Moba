using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ForceController : SkillController
{
    public int MinValue;
    public int MaxValue;


    private string _description
    {
        get
        {
            Dict = new Dictionary<int, string>();
            Dict[Language.Portuguese] = "Utiliza uma rasteira contra os oponentes ao seu redor e causa dano de acordo com a Força acumulada (Mínimo 80%STR ({0}) - Máximo 150%STR ({1})).";
            Dict[Language.English] = "Description is invalid or null.";
            return Dict[Language.CurrentLanguage];
        }
    }

    private float GetMinDamage()
    {
        return base.Owner.Stats.Strenght * 0.8f;
    }

    private float GetMaxDamage()
    {
        return base.Owner.Stats.Strenght * 1.5f;
    }

    private float GetDamage()
    {
        PowerForce force = base.Owner.gameObject.GetComponent<PowerForce>();
        float modifier = force.Value * (MaxValue - MinValue) / 100;
        modifier += MinValue;

        float newdamage = base.Owner.Stats.Strenght * modifier / 100;
        return newdamage;
    }


    public override string GetName()
    {
        Dict = new Dictionary<int, string>();
        Dict[Language.Portuguese] = "Força";
        Dict[Language.English] = "Force";
        return Dict[Language.CurrentLanguage];
    }

    public override string GetDescription()
    {
		return string.Format(_description, Math.Round(GetMinDamage(), 0), Math.Round(GetMaxDamage(), 0));
    }

    public override string GetNextDescription()
    {
        return GetDescription();
    }

    public override void Damage(Player target)
    {
        target.TakeDamage(base.Settings.Damage.Type, GetDamage(), base.Owner);
    }
}
