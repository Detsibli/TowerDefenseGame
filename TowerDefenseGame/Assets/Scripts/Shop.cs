using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laser1;

    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectBasic1Turret()
    {
        Debug.Log("Basic LVL. 1");
        buildManager.SelectTurretToBuild(standardTurret);
    }
    public void SelectMissleTurret()
    {
        Debug.Log("Missle LVL. 1");
        buildManager.SelectTurretToBuild(missileLauncher);
    }
    public void SelecLaser1Turret()
    {
        Debug.Log("Laser LVL. 1");
        buildManager.SelectTurretToBuild(laser1);
    }
}
