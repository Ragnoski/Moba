using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISliderController : MonoBehaviour
{
    private Slider _slider;
    private Text _slidertext;
    private float _amount;
    private float _time;
    private string _originaltext;
    private bool _iscounting;


    private void Awake()
    {
        _slidertext = GetComponentInChildren<Text>();
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _originaltext = _slidertext.text;
    }

    private IEnumerator Counter()
    {
        _iscounting = true;
        _slider.value = 100;

        for (float i = _time; i >= 0; i--)
        {
            yield return new WaitForSeconds(0.1f);
            if ( i >= 10)
            {
                _slidertext.text = ((int)(i / 10)).ToString();
            }
            else
            {
                _slidertext.text = (i / 10).ToString();
            }

            _slider.value -= _amount;
        }

        _iscounting = false;
        _slider.value = 0;
        _slidertext.text = _originaltext;
    }


    public bool IsCounting()
    {
        return _iscounting;
    }

    public void Count(int time)
    {
        _time = (time * 10f) + 9f;
        _amount = 1f * 100f / _time;
        _slidertext.text = time.ToString();
        StartCoroutine(Counter());
    }

    public void Show()
    {
        _slidertext.color = new Color(_slidertext.color.r, _slidertext.color.g, _slidertext.color.b, 255f);
    }
}
