using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    public Text StatusText;

    private SkillController[] _skillcontrollers;
    private PlayerMovement _playermovement;
    private Player _player;
    private float _lifetime;
    private float _damage;
    private bool _paralyzed;
    private bool _bleeding;
    private bool _sleeping;
    private bool _idle;
    private Player _hitby;


    private void Awake()
    {
        _skillcontrollers = GetComponents<SkillController>();
        _playermovement = GetComponent<PlayerMovement>();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        StatusText.text = "";
    }

    private void EnableAllControls(bool value)
    {
        for (int i = 0; i < _skillcontrollers.Length; i++)
        {
            _skillcontrollers[i].enabled = value;
        }

        if (_playermovement)
            _playermovement.enabled = value;
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
            _player.TakeDamage(Stats.Damage, amount, _hitby);
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

        if(_sleeping == true)
        {
            _sleeping = false;
            StatusText.text = string.Empty;
            EnableAllControls(true);
        }
    }


    // can't move or attack until time does not end
    public void Paralyze(float lifetime)
    {
        _lifetime = lifetime;
        StartCoroutine(ParalyzeCounter());
    }

    // lose life during lifetime
    public void Bleed(float lifetime, float bleedamount, Player hitby)
    {
        _lifetime = lifetime;
        _damage = bleedamount;
        _hitby = hitby;
        StartCoroutine(BleedCounter());
    }

    // can't move or attack until hit by either enemy attack or skill
    public void Sleep(float lifetime)
    {
        _lifetime = lifetime;
        StartCoroutine(SleepCounter());
    }

    // can't move attack or use skills until idle is true
    public void Idle(bool value)
    {
        _idle = value;
        EnableAllControls(!value);
    }

    public void WakeUp()
    {
        if (_sleeping)
        {
            StopCoroutine("SleepCounter");

            _sleeping = false;
            StatusText.text = string.Empty;
            EnableAllControls(true);
        }
    }
}
