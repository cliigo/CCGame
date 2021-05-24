using System;
using System.Collections;
using System.Collections.Generic;
using CCFrameWork.Base.Game;
using CCFrameWork.Common.Pipeline;
using CCGame.UI.Loading;
using UnityEngine;

public class GameInit : MonoBehaviour
{

    [SerializeField] public GameObject view_root;
    
    
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
        Game.Init(view_root);
    }

    public bool Test(Action a, Action b)
    {
        return a == b;
    }

    void Update()
    {
        Game.Update();
    }

    private void LateUpdate()
    {
        Game.LateUpdate();
    }
    
    
}
