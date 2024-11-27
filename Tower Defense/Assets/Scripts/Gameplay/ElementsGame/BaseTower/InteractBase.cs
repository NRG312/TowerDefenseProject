using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractBase : MonoBehaviour
{
    private Ray _ray;
    private RaycastHit _hit;
    private GameObject _target;
    private Camera _cam;
    [Header("Toolkit")] 
    [SerializeField] private GameObject toolKit;
    [SerializeField] private GameObject buyPanel;
    [SerializeField] private GameObject upgradePanel;

    private void Start()
    {
        _cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray,out _hit))
            {
                if (_hit.collider.tag == "BaseTower")
                {
                    _target = _hit.collider.gameObject;
                    toolKit.SetActive(true);
                    if (_target.GetComponent<BaseController>().CheckEmptySlot())
                    {
                        buyPanel.SetActive(true);
                        buyPanel.GetComponent<BuyNewTower>().NewTarget(_target);
                        //
                        upgradePanel.SetActive(false);
                    }
                    else
                    {
                        upgradePanel.SetActive(true);
                        //Send tower component to upgrade
                        //
                        buyPanel.SetActive(false);
                    }
                }
                else if(_hit.collider.tag != "BaseTower" && !EventSystem.current.IsPointerOverGameObject())
                {
                    _target = null;
                    toolKit.SetActive(false);
                }
            }
        }
    }
}
