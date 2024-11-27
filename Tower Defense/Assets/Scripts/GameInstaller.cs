using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public GameObject managersPrefab;
    public TowerController[] listTowers;
    public GameObject[] listEnemy;
    public override void InstallBindings()
    {
        CreateManagerObject();
    }

    private void CreateManagerObject()
    {
        //Create managers
        InstallManagers();
        InstallGameplay();
    }
    private void InstallGameplay()
    {
        foreach (var t in listTowers)
        {
            Container.BindFactory<TowerController, TowerController.Factory>().FromComponentInNewPrefab(t);
        }

        /*foreach (var t in listEnemy)
        {
            //Container.BindFactory<>()
        }*/
    }

    private void InstallManagers()
    {
        Container.Bind<GameplayController>().FromComponentInHierarchy().AsSingle();
        //
        Container.Bind<ScoreController>().FromComponentInHierarchy().AsSingle();
    }
}
