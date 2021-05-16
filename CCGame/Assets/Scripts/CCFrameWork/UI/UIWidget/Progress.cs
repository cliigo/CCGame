using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;





public class Progress : MonoBehaviour
{

    public delegate string UpdateProgressDelegate(float progress);

    // Start is called before the first frame update
    public Image progress_bg;
    public Image progress_bar;
    public Text progress_text;
    public float progress_speed = 5.0f;

    [NonSerialized] public UpdateProgressDelegate _txt_update_action;
    [NonSerialized] public float _cur_progress = 0;
    [NonSerialized] public float _next_progress = 0;
    [NonSerialized] public float _max_bar_width;
    [NonSerialized] public float _max_bar_height;

    private void Awake()
    {
        _max_bar_width = progress_bg.rectTransform.sizeDelta.x;
        _max_bar_height = progress_bg.rectTransform.sizeDelta.y;
    }

    public void InitProgress() 
    {
        InitProgress(_update_progrees_txt_default);
    }

    public void InitProgress(UpdateProgressDelegate txt_update_action) {
        _cur_progress = 0;
        _next_progress = 0;
        _txt_update_action = txt_update_action;
    }

    public void MoveToProgress(float progress) 
    {
        _next_progress = progress;
    }

    // Update is called once per frame
    void Update()
    {
        if (_cur_progress < _next_progress) 
        {
            _cur_progress += progress_speed * Time.deltaTime;
            _cur_progress = Math.Min(_cur_progress, _next_progress);
            _update_Progress();
        }
    }

    public void _update_Progress() 
    {
        progress_bar.rectTransform.sizeDelta = new Vector2(_cur_progress * _max_bar_width, _max_bar_height);
        if (progress_text) 
        {
            progress_text.text = _txt_update_action(_cur_progress);
        }
    }

    public string _update_progrees_txt_default(float progress) 
    {
        return String.Format(" {0:N2} %", progress * 100);
    }
}
