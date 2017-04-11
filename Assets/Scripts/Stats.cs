using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Stats
{
    public bool STR;
    public bool INT;
    public bool HIT;
    public bool DAM;

    static public Stats Strength
    {
        get
        {
            Stats temp = new Stats();
            temp.STR = true;
            return temp;
        }
    }

    static public Stats Inteligence
    {
        get
        {
            Stats temp = new Stats();
            temp.INT = true;
            return temp;
        }
    }

    static public Stats Hit
    {
        get
        {
            Stats temp = new Stats();
            temp.HIT = true;
            return temp;
        }
    }

    static public Stats Damage
    {
        get
        {
            Stats temp = new Stats();
            temp.DAM = true;
            return temp;
        }
    }
}

[System.Serializable]
public class Aptittude
{
    public float Life;
    public float Strenght;
    public float Defense;
    public float Inteligence;
    public float Resistance;
    public float Hit;
    public float Speed;
    public float Critical;
    public float Attack;
    public float Velocity;
}

[System.Serializable]
public class PlayerStats
{
    public RectTransform LifeUI;
    public Text LifeText;

    public float BaseLife;
    public float BaseStrenght;
    public float BaseDefense;
    public float BaseInteligence;
    public float BaseResistance;
    public float BaseHit;
    public float BaseSpeed;
    public float BaseCritical;
    public float BaseAttackSpeed;
    public float BaseVelocity;
    public float BaseLifeRegen;

    public float Life { get { return _life; } }
    public float Strenght {  get { return _strenght; } }
    public float Defense {  get { return _defense; } }
    public float Inteligence { get { return _inteligence; } }
    public float Resistance { get { return _resistance; } }
    public float Hit { get { return _hit; } }
    public float Speed { get { return _speed; } }
    public float Critical { get { return _critical; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public float Velocity { get { return _velocity; } }


    private float _life;
    private float _maxLife;
    private float _strenght;
    private float _defense;
    private float _inteligence;
    private float _resistance;
    private float _hit;
    private float _speed;
    private float _critical;
    private float _attackSpeed;
    private float _velocity;
    private float _lifeRegen;


    private void UpdateUI()
    {
        float newlifescale = _life * 1f / _maxLife;
        LifeUI.localScale = new Vector3(newlifescale, LifeUI.localScale.y, LifeUI.localScale.z);
        LifeText.text = (int)_life + " / " + (int)_maxLife;
    }


    public void Initialize()
    {
        _life = BaseLife;
        _maxLife = BaseLife;
        _strenght = BaseStrenght;
        _defense = BaseDefense;
        _inteligence = BaseInteligence;
        _resistance = BaseResistance;
        _hit = BaseHit;
        _speed = BaseSpeed;

        _critical = BaseCritical;
        _attackSpeed = BaseAttackSpeed;
        _velocity = BaseVelocity;

        _lifeRegen = BaseLifeRegen;

        UpdateUI();
    }

    public void IncreaseBy(Aptittude percent)
    {
        _maxLife += BaseLife * percent.Life / 100;
        _strenght += BaseStrenght * percent.Strenght / 100;
        _defense += BaseDefense * percent.Defense / 100;
        _inteligence += BaseInteligence * percent.Inteligence / 100;
        _resistance += BaseResistance * percent.Resistance / 100;
        _hit += BaseHit * percent.Hit / 100;
        _speed += BaseSpeed * percent.Speed / 100;
        _critical += BaseCritical * percent.Critical / 100;
        _attackSpeed += BaseAttackSpeed * percent.Attack / 100;
        _velocity += BaseVelocity * percent.Velocity / 100;

        UpdateUI();
    }

    public void Defend(float amount)
    {
        float value = amount - _defense;
        Damage(value);
    }

    public void Resist(float amount)
    {
        float value = amount - _resistance;
        Damage(value);
    }

    public void Escape(float amount)
    {
        float value = amount - _speed;
        Damage(value);
    }

    public void Damage(float value)
    {
        if (value > 0)
        {
            _life -= value;
        }

        UpdateUI();
    }
}
