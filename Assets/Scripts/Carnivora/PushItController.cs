using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushItController : SkillController
{
    private string _description
    {
        get
        {
            Dict = new Dictionary<int, string>();
            Dict[Language.Portuguese] = "Avança em linha reta e para no primeiro inimigo atingido. Causa {0} STR de dano e paralisa o inimigo por {1} segundo(s).";
            Dict[Language.English] = "Description is invalid or null.";
            return Dict[Language.CurrentLanguage];
        }
    }


    public override string GetName()
    {
        Dict = new Dictionary<int, string>();
        Dict[Language.Portuguese] = "Empurrão";
        Dict[Language.English] = "Push it";
        return Dict[Language.CurrentLanguage];
    }

    public override string GetDescription()
    {
        return string.Format(_description, base.Settings.Damage.Value, base.Settings.Paralyze.Time);
    }

    public override string GetNextDescription()
    {
        return string.Format(_description, base.NextSettings.Damage.Value, base.NextSettings.Paralyze.Time);
    }

    public override void Damage(Player target)
    {
        target.TakeDamage(base.Settings.Damage.Type, base.Settings.Damage.Value, base.Owner);
        target.Paralyze(base.Settings.Paralyze.Time);
    }
}
