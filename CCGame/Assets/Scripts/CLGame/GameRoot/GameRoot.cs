using System;
using CLFramework.Game;
using UnityEngine;

namespace CLGame.GameRoot
{
    public class GameRoot : MonoBehaviour
    {
        private void Start()
        {
            GameIns.ui_manager.Init(gameObject);
        }

        private void Update()
        {
            GameIns.Update();
        }
    }
}