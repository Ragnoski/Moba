using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string Name;
    public PlayerStats Stats;
    public Aptittude Aptittude;
    public Text StatusText;


    private int _level;
    private int _experience;
    private int _nextLevelExp;
    private SkillsManager _skillsmanager;
    private Clicktomove _clicktomove;
    private Power _power;

    private bool _paralyzed;
    private bool _bleeding;
    private bool _sleeping;
    private bool _idle;
    private float _lifetime;
    private float _damage;
    private Player _hitby;


    private void Awake()
    {
        _skillsmanager = GetComponent<SkillsManager>();
        _clicktomove = GetComponent<Clicktomove>();
        _power = GetComponent<Power>();
    }

    private void Start()
    {
        _level = 0;
        _experience = 0;
        _nextLevelExp = 50;

        StatusText.text = "";

        Stats.Initialize();
    }

    private void EnableAllControls(bool value)
    {
        _skillsmanager.EnableAll(value);

        if (_clicktomove)
            _clicktomove.Enable(value);
    }

    private IEnumerator ParalyzeCounter()
    {
        _paralyzed = true;
        StatusText.text = "Paralyzed";
        EnableAllControls(false);

        yield return new WaitForSeconds(_lifetime);

        _paralyzed = false;
        StatusText.text = string.Empty;
        EnableAllControls(true);
    }

    private IEnumerator BleedCounter()
    {
        _bleeding = true;
        StatusText.text = "Bleeding";

        int time = (int)(_lifetime * 10) + 9;
        float amount = 1 * _damage / _lifetime;

        for (int i = time; i > 0; i--)
        {
            yield return new WaitForSeconds(0.1f);
            TakeDamage(SCTStats.Damage, amount, _hitby);
        }

        _bleeding = false;
        StatusText.text = string.Empty;
    }

    private IEnumerator SleepCounter()
    {
        _sleeping = true;
        StatusText.text = "Sleeping";
        EnableAllControls(false);

        yield return new WaitForSeconds(_lifetime);

        if (_sleeping == true)
        {
            _sleeping = false;
            StatusText.text = string.Empty;
            EnableAllControls(true);
        }
    }

    private void WakeUp()
    {
        if (_sleeping)
        {
            StopCoroutine("SleepCounter");

            _sleeping = false;
            StatusText.text = string.Empty;
            EnableAllControls(true);
        }
    }

    private void OnDeath()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
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

    public void AddExperience(int newvalue)
    {
        _experience += newvalue;
        if (_experience >= _nextLevelExp)
        {
            LevelUp();
        }
    }

    // can't move or attack until time does not end
    public void Paralyze(float lifetime)
    {
        if (!gameObject.activeSelf)
            return;

        _lifetime = lifetime;
        StartCoroutine(ParalyzeCounter());
    }

    // lose life during lifetime
    public void Bleed(float lifetime, float bleedamount, Player hitby)
    {
        if (!gameObject.activeSelf)
            return;

        _lifetime = lifetime;
        _damage = bleedamount;
        _hitby = hitby;
        StartCoroutine(BleedCounter());
    }

    // can't move or attack until hit by either enemy attack or skill
    public void Sleep(float lifetime)
    {
        if (!gameObject.activeSelf)
            return;

        _lifetime = lifetime;
        StartCoroutine(SleepCounter());
    }

    // can't move attack or use skills until idle is true
    public void Idle(bool value)
    {
        if (!gameObject.activeSelf)
            return;

        _idle = value;
        EnableAllControls(!value);
    }

    public void TakeDamage(SCTStats type, float amount, Player hitBy)
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

        WakeUp();

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
