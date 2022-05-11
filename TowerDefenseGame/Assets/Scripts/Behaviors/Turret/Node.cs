using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color nobuildColor;
    public Color notEnoughMoneyColor;
    private Color startColor;
    private Renderer rend;
	public AudioSource buildSFX;
	public AudioSource sellSFX;
	public AudioSource upgradeSFX;
    
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;
	[HideInInspector]

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        hoverColor = Color.green;
        nobuildColor = Color.red;
        notEnoughMoneyColor = Color.yellow;
        
        buildManager = BuildManager.instance;
    }
   public Vector3 GetBuildPosition()
    {
        return transform.position;
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (turret != null)
        {
            rend.material.color = nobuildColor;
            buildManager.SelectedNode(this);
            return;
        }
        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if(PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("not enough money");
            return;
        }
        PlayerStats.Money -= blueprint.cost;

		buildSFX.Play();
        
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.BuildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);
        
        Debug.Log("Turret Build!");
    }

    public void UpgradeTurret()
    {
		if(PlayerStats.Money < turretBlueprint.upgradeCost)
		{
			Debug.Log("Not enough money to upgrade");
			return;
		}
			PlayerStats.Money -= turretBlueprint.upgradeCost;

			//Delete old turret
			Destroy(turret);
				
			upgradeSFX.Play();
			
			//building new turret
			GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
			turret = _turret;
			GameObject effect = (GameObject)Instantiate(buildManager.BuildEffect, GetBuildPosition(), Quaternion.identity);
			Destroy(effect, 3f);

			isUpgraded = true;

			Debug.Log("Turret Upgraded!");
    }

	public void SellTurret()
	{
		PlayerStats.Money += turretBlueprint.GetSellAmount();

		sellSFX.Play();

		GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 3f);

		Destroy(turret);
		turretBlueprint = null;
		isUpgraded = false;
	}

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.CanBuild)       
            return;
        if(buildManager.HasMoney)
            rend.material.color = hoverColor;
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
