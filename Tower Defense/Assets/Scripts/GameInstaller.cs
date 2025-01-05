using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public TowerController[] listTowers;
    public GameObject[] listEnemy;
    
    public override void InstallBindings()
    {
        CreateInstallsObject();
    }
    
    private void CreateInstallsObject()
    {
        InstallManagers();
        InstallGameplay();
        InstallSignals();
    }

    private void InstallSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<OnWinGame>();
        Container.DeclareSignal<OnLoseGame>();
    }
    private void InstallGameplay()
    {
        foreach (var t in listTowers)
        {
            Container.BindFactory<TowerController, TowerController.Factory>().FromComponentInNewPrefab(t);
        }

        foreach (var t in listEnemy)
        {
            Container.BindFactory<EnemyController, EnemyController.Factory>().FromComponentInNewPrefab(t);
        }
    }

    private void InstallManagers()
    {
        Container.Bind<GameplayController>().FromComponentInHierarchy().AsSingle();
        //
        Container.Bind<WaveController>().FromComponentInHierarchy().AsSingle();
        //
        Container.Bind<ObjectsTickController>().FromComponentInHierarchy().AsSingle();
        //
        Container.Bind<EnemySpawner>().FromComponentInHierarchy().AsSingle();
        //
        Container.Bind<WaypointsController>().FromComponentInHierarchy().AsSingle();
        //
        Container.Bind<UIController>().FromComponentInHierarchy().AsSingle();
        //
        Container.Bind<MoneyController>().FromComponentInHierarchy().AsSingle();
        //
        Container.Bind<ObjectPool>().FromComponentInHierarchy().AsSingle();
        
    }
}
