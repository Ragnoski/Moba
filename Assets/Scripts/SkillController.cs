using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DamageSettings
{
    public float Value;
    public SCTStats Type;
}

[System.Serializable]
public class StatusSettings
{
    public float Time;
    public DamageSettings DamageOverTime;
}

[System.Serializable]
public class SkillSettings
{
    public int LevelRequired;
    public int Delay;
    public int Cost;
    public StatusSettings Paralyze;
    public StatusSettings Sleep;
    public StatusSettings Bleed;
    public DamageSettings Damage;
}

public class SkillController : MonoBehaviour
{
    public string FireButton;
    public SkillSettings[] SettingsByLevel;
    public UISliderController SliderController;
    public Slider LevelSlider;
    public Image UpgradeImage;
    public GameObject SkillPrefab;
    public Transform SpawnPoint;

    [HideInInspector] public Player Owner;
    [HideInInspector] public Dictionary<int, string> Dict;

    public int Level { get { return _settingsindex + 1; } }
    public SkillSettings Settings { get { return SettingsByLevel[_settingsindex]; } }
    public SkillSettings NextSettings { get { return SettingsByLevel[_settingsindex + 1]; } }


    private const int _maxupgrade = 3;

    private int _settingsindex;
    private bool _controlButton;
    private bool _canupgrade;
    private Power _power;
    private SkillsManager _skillsmanager;


    private void Awake()
    {
        _power = GetComponent<Power>();
        _skillsmanager = GetComponent<SkillsManager>();
        Owner = GetComponent<Player>();
    }

    private void Start()
    {
        _controlButton = false;
        _settingsindex = -1;
    }

    private void Upgrade()
    {
        _settingsindex += 1;

        _skillsmanager.DisableSkillsUpgrade();

        LevelSlider.value = Level;
        SliderController.Show();
    }

    private bool CanFire()
    {
        if (SliderController.IsCounting())
            return false;

        if (_settingsindex < 0)
            return false;

        return _power.HasValue(Settings.Cost);
    }

    private void Fire()
    {
        _power.Decrease(Settings.Cost);
        SliderController.Count(Settings.Delay);
    }

    private void Play()
    {
        GameObject prefab = Instantiate(SkillPrefab, SpawnPoint.position, SpawnPoint.rotation, gameObject.transform);
        PlayerSkill playerskill = prefab.GetComponent<PlayerSkill>();
        playerskill.Controller = this;

        if (playerskill.CanPlay())
        {
            playerskill.Play();
            Fire();
        }
        else
        {
            Destroy(prefab);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _controlButton = true;
        }
        else
        {
            _controlButton = false;
        }

        if (Input.GetButtonDown(FireButton))
        {
            if(_canupgrade && _controlButton)
            {
                Upgrade();
            }
            else if(!_controlButton)
            {
                //SkillArea.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        else if (Input.GetButtonUp(FireButton))
        {
            //SkillArea.GetComponent<MeshRenderer>().enabled = false;

            if (!_controlButton && CanFire())
            {
                Play();
            }
        }
    }


    public void EnableUpgrade(int level)
    {
        if (_settingsindex >= _maxupgrade)
        {
            _canupgrade = false;
        }
        else if (NextSettings.LevelRequired <= level)
        {
            _canupgrade = true;
            UpgradeImage.enabled = true;
        }
    }

    public void DisableUpgrade()
    {
        _canupgrade = false;
        UpgradeImage.enabled = false;
    }


    virtual public string GetName()
    {
        return "Name is invalid or null.";
    }

    virtual public string GetNextDescription()
    {
        return "Description is invalid or null";
    }

    virtual public string GetDescription()
    {
        return "Description is invalid or null.";
    }

    virtual public void Damage(Player target)
    {
        throw new System.Exception("Damage method not implemented.");
    }
}
