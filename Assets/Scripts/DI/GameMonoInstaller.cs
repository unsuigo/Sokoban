using System.Collections;
using System.Collections.Generic;
using Sokoban;
using Sokoban.Data;
using Sokoban.Service;
using UnityEngine;
using Zenject;

namespace Sokoban.Di
{
   
    public class GameMonoInstaller : MonoInstaller<GameMonoInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<FieldContainer>().FromComponentInHierarchy().AsSingle();
            Container.Bind<InputSystem>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ScoreService>().FromComponentInHierarchy().AsSingle();
            Container.Bind<SoundService>().FromComponentInHierarchy().AsSingle();
            Container.Bind<DataManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}
