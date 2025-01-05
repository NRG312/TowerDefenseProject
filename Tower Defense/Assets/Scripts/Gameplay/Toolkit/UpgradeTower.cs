using TMPro;
using UnityEngine;
using Zenject;

public class UpgradeTower : MonoBehaviour
{
    [SerializeField] private GameObject sfxUpgradingTower;
    [Space(20f)]
    [SerializeField] private TMP_Text damageTxt;
    [SerializeField] private TMP_Text speedShotTxt;
    [SerializeField] private TMP_Text priceTxt;
    [Space(20)] 
    [SerializeField] private GameObject backGroundTools;
    
    [Inject] private TowerController.Factory[] _createTower;
    [Inject] private GameplayController _gameplayController;
    
    
    private GameObject _baseTarget;
    private TowerDataSO _newTowerData;
    //
    private float _oldDamage, _newDamage;
    private float _oldSpeedShot, _newSpeedShot;
    private float _upgradePrice;
    //
    
    
    private void Update()
    {
        if (backGroundTools.activeInHierarchy)
        {
            //set data
            TowerDataSO oldTower = _baseTarget.GetComponentInChildren<TowerController>().towerData;
            TowerDataSO newTower = _baseTarget.GetComponentInChildren<TowerController>().towerData.towerUpgraded;
            _oldDamage = oldTower.damage;
            _newDamage = newTower.damage;
            _oldSpeedShot = oldTower.timeToShoot;
            _newSpeedShot = newTower.timeToShoot;
            _upgradePrice = newTower.price;
            //Set texts in GUI
            float additionDamage = _newDamage - _oldDamage; 
            damageTxt.text = "Damage: " + "<color=red>" + _oldDamage.ToString("F2") +"</color>"+ " + " + "<color=green>" + additionDamage +"</color>";
            var additionSpeedShot = _oldSpeedShot - _newSpeedShot;
            speedShotTxt.text = "SpeedShot: "+ "<color=red>" + _oldSpeedShot + "s" + "</color>" + " - " + "<color=green>" + additionSpeedShot + "s" +"</color>";
            //
            if (_gameplayController.Money < _newTowerData.price)
            {
                priceTxt.text = "<color=red>" + "$" + _upgradePrice.ToString() + "</color>";
            }
            else if(_gameplayController.Money >= _newTowerData.price)
            {
                priceTxt.text = "<color=green>" + "$" + _upgradePrice.ToString() + "</color>";
            }
        }
    }
    //
    public void NewTarget(GameObject target)
    {
        _baseTarget = target;
        _newTowerData = _baseTarget.GetComponentInChildren<TowerController>().towerData.towerUpgraded;
    }

    public void UpgradeTurret()
    {
        if (_gameplayController.Money >= _newTowerData.price )
        {
            GameObject oldTower = _baseTarget.transform.GetChild(0).gameObject;
            
            //Replace old tower
            TowerController newTower = _createTower[_newTowerData.index].Create();
            //Position
            newTower.transform.SetParent(_baseTarget.transform);
            newTower.gameObject.transform.position = _baseTarget.transform.position;
            //Rotation
            newTower.transform.rotation = _baseTarget.transform.rotation;
            
            //Delete old tower from objectsTickController
            ObjectsTickController.onChangeTower.Invoke(oldTower.GetComponent<TowerController>());
            Destroy(oldTower);
            ObjectsTickController.onCreateTower.Invoke(newTower);
            
            //
            _gameplayController.RemoveMoney(_newTowerData.price);
            //
            backGroundTools.SetActive(false);
        }
    }
}
