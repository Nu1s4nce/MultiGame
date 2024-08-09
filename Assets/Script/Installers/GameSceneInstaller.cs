using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<HPBar>().FromNew().AsSingle();
    }
}
