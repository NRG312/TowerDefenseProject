using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StateAttack : State
{
    [SerializeField] private StateWaiting stateWaiting;
    private TowerController _towerController;
    private GameObject _tower;
    public override State RunState()
    {
        RunTower();
        if (_towerController.target == null)
        {
            return stateWaiting;
        }
        else
        {
            return this;
        }
    }

    public override void TransitionData(TowerController towerController,GameObject tower)
    {
        _towerController = towerController;
        _tower = tower;
    }
    
    private void RunTower()
    {
        //Skonczylem na zrobieniu przeciwnika i wiezyczek wszystko dziala dobrze zabijaja i przestaja strzelac jak go zabija(musze zobaczyc czy moze nie daloby sie tego zrobic poprzez event zeby moze nie byl wykrywalny czy cos)
        //jest blad ze jak postawie wiezyczke to spherecollider blokuje klikniecie w inne plytki musze wylaczyc blokade na nich
        //mam pomysl jak odpale collision w wiezy laser (desktop/Laser) to sie pociski odbijaja to mozna dodac do niektorych wiez jesli maja za maly level to np beda odbijac pociski
        //RocketLauncher nie ma partcilesystem bedzie trzeba zobaczyc i przemyslec go(moze uda mi sie dodać do shoot function ze przy wykryciu ze nie ma partcile system to stworzy pocisk ktory tutaj doda sie recznie)
        if (_towerController.target != null)
        {
            _tower.transform.DOLookAt(_towerController.target.transform.position,1);
            if (Physics.Raycast(_tower.transform.position,_tower.transform.forward,10))
            {
                var angle = Vector3.Angle((_towerController.target.transform.position - _tower.transform.position),
                    _tower.transform.forward);
                
                if (angle < 8f && _towerController.canShoot)
                {
                    Shoot();
                }
            }
        }
        //if target is destroyed change to null
        if (!_towerController.target.gameObject.activeInHierarchy && _towerController.target != null)
        {
            _towerController.target = null;
        }
    }

    private void Shoot()
    {
        ParticleSystem particle = _tower.GetComponentInChildren<ParticleSystem>();
        particle.Play();
        _towerController.canShoot = false;
    }
}
