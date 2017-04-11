using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name;
    public PlayerStats Stats;
    public Aptittude Aptittude;


    private int _level;
    private int _experience;
    private int _nextLevelExp;
    private StatusController _statuscontroller;
    private SkillsManager _skillsmanager;
    private Power _power;


    private void Awake()
    {
        _statuscontroller = GetComponent<StatusController>();
        _skillsmanager = GetComponent<SkillsManager>();
        _power = GetComponent<Power>();
    }

    private void Start()
    {
        _level = 0;
        _experience = 0;
        _nextLevelExp = 50;

        Stats.Initialize();
    }

    public void LevelUp()
    {
        _level += 1;
        _nextLevelExp *= 2;

        Stats.IncreaseBy(Aptittude);

        if(_skillsmanager)
            _skillsmanager.EnableSkillsUpgrade(_level);

        Debug.Log(_level);
    }

    private void OnDeath()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }


    public void AddExperience(int newvalue)
    {
        _experience += newvalue;
        if (_experience >= _nextLevelExp)
        {
            LevelUp();
        }
    }

    public void Paralyze(float time)
    {
        if (!gameObject.activeSelf)
            return;

        _statuscontroller.Paralyze(time);
    }

    public void Bleed(float lifetime, float bleedamount, Player hitby)
    {
        if (!gameObject.activeSelf)
            return;

        _statuscontroller.Bleed(lifetime, bleedamount, hitby);
    }

    public void Sleep(float time)
    {
        if (!gameObject.activeSelf)
            return;

        _statuscontroller.Sleep(time);
    }

    public void Idle(bool value)
    {
        if (!gameObject.activeSelf)
            return;

        _statuscontroller.Idle(value);
    }

    public void TakeDamage(Stats type, float amount, Player hitBy)
    {
		if (type.STR) {
			Stats.Defend (amount);
		} else if (type.INT) {
			Stats.Resist (amount);
		} else if (type.HIT) {
			Stats.Escape (amount);
		} else if (type.DAM) {
			Stats.Damage (amount);
		} else {
			throw new System.Exception("Damage type is invalid or null.");
		}

        _statuscontroller.WakeUp();

        if(Stats.Life <= 0f)
        {
            OnDeath();
        }
    }

    public string GetNextSkillDescription(string firebutton)
    {
        return _skillsmanager.GetSkillDescription(firebutton, next: true);
    }

    public string GetSkillDescription(string firebutton)
    {
        return _skillsmanager.GetSkillDescription(firebutton);
    }
}
