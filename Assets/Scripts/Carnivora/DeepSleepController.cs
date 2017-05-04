using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeepSleepController : SkillController
{
    private string _description
    {
        get
        {
            Dict = new Dictionary<int, string>();
            Dict[Language.Portuguese] = "Lança um pó no ar ao seu redor que causa dano igual a 150%INT ({0}) e coloca os inimigos atingidos para dormir por {1} segundo(s).";
            Dict[Language.English] = "Description is invalid or null.";
            return Dict[Language.CurrentLanguage];
        }
    }

    private float GetDamage()
    {
        return base.Owner.Stats.Inteligence * 1.5f;
    }


    public override string GetName()
    {
        Dict = new Dictionary<int, string>();
        Dict[Language.Portuguese] = "Sono Profundo";
        Dict[Language.English] = "Deep Sleep";
        return Dict[Language.CurrentLanguage];
    }

    public override string GetDescription()
    {
		return string.Format(_description, Math.Round(GetDamage(), 0), base.Settings.Sleep.Time);
    }

    public override string GetNextDescription()
    {
		return string.Format(_description, Math.Round(GetDamage(), 0), base.NextSettings.Sleep.Time);
    }

    public override void Damage(Player target)
    {
        target.TakeDamage(base.Settings.Damage.Type, GetDamage(), base.Owner);
        target.Sleep(base.Settings.Sleep.Time);
    }
}
