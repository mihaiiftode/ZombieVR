using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class Player : MonoBehaviour
{

    public float Health = 100;
    public Text HealthText;
    private UIFader uiFader;

	// Use this for initialization
	void Start ()
	{
	    var weapon = GameObject.Find("ShooterWeapon");
	    uiFader = weapon.GetComponent<UIFader>();
        HealthText.text = "Health: " + Health;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void TakeDamage(float attackDamage)
    {
        Health -= attackDamage;
        HealthText.text = "Health: " + Health;
    }
}
