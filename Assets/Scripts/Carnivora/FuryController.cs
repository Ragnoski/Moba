using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FuryController : SkillController
{
    private string _description
    {
        get
        {
            Dict = new Dictionary<int, string>();
            Dict[Language.Portuguese] = "Arranha as presas ao seu redor causando 2xSTR ({0}) e sangramento 50%STR ({1}) ao longo de {2} segundo(s)";
            Dict[Language.English] = "Description is invalid or null.";
            return Dict[Language.CurrentLanguage];
        }
    }

    private float GetDamage()
    {
        return base.Owner.Stats.Strenght * 2f;
    }

    private float GetBleedingDamage()
    {
        float bleedamount = base.Owner.Stats.Strenght * 0.5f;
        return bleedamount;
    }


    public override string GetName()
    {
        Dict = new Dictionary<int, string>();
        Dict[Language.Portuguese] = "Fúria";
        Dict[Language.English] = "Fury";
        return Dict[Language.CurrentLanguage];
    }

    public override string GetDescription()
    {
		return string.Format(_description, Math.Round(GetDamage(), 0), Math.Round(GetBleedingDamage(), 0), base.Settings.Bleed.Time);
    }

    public override string GetNextDescription()
    {
		return string.Format(_description, Math.Round(GetDamage(), 0), Math.Round(GetBleedingDamage(), 0), base.NextSettings.Bleed.Time);
    }

    public override void Damage(Player target)
    {
        target.TakeDamage(base.Settings.Damage.Type, GetDamage(), base.Owner);
        target.Bleed(base.Settings.Bleed.Time, GetBleedingDamage(), base.Owner);
    }
}
