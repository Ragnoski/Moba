using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    private SkillController[] _skillcontrollers;
    private int _upgradesleft;
    private int _playerlevel;


    private void Awake()
    {
        _skillcontrollers = GetComponents<SkillController>();
    }

    private void Start()
    {
        _upgradesleft = 0;
    }

    private void EnableUpgrade()
    {
        foreach (SkillController scontrol in _skillcontrollers)
        {
            scontrol.EnableUpgrade(_playerlevel);
        }
    }

    private void DisableUpgrade()
    {
        foreach (SkillController scontrol in _skillcontrollers)
        {
            scontrol.DisableUpgrade();
        }
    }


    public void EnableSkillsUpgrade(int level)
    {
        _upgradesleft += 1;
        _playerlevel = level;
        if (_upgradesleft > 1)
        {
            DisableUpgrade();
        }

        EnableUpgrade();
    }

    public void DisableSkillsUpgrade()
    {
        _upgradesleft -= 1;
        DisableUpgrade();

        if (_upgradesleft > 0)
        {
            EnableUpgrade();
        }
    }

    public string GetSkillDescription(string firebutton, bool next = false)
    {
        foreach (SkillController scontrol in _skillcontrollers)
        {
            if (scontrol.FireButton == firebutton)
            {
                if (next)
                    return scontrol.GetNextDescription();

                else
                    return scontrol.GetDescription();
            }
        }

        return string.Empty;
    }
}
