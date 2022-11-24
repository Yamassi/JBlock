using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamber2 : MonoBehaviour
{
    private float maxAmmoPosY = 43;
    private float oldAmmoPosY;
    private float currentAmmoPosY;
    private float ammoInChamber;
    private Vector3 chamberScale;
    [SerializeField] float maxAmmo;
    Transform chamber2;
    Animator animator;

    // ChamberTransparent chamberTransparent;
    private void Awake()
    {
        chamber2 = transform;
        chamberScale = new Vector3(chamber2.localScale.x, maxAmmoPosY, chamber2.localScale.z);
        animator = chamber2.GetComponent<Animator>();
        if (currentAmmoPosY >= 43)
        {
            currentAmmoPosY = 43;
            chamber2.localScale = new Vector3(chamberScale.x, currentAmmoPosY, chamberScale.z);
        }
        // print(chamberScale);
        // chamberTransparent = FindObjectOfType<ChamberTransparent>();
    }
    public void SetAmmoInChamber(float ammo)
    {
        // oldAmmoPosY = chamber.localScale.y;
        currentAmmoPosY = (ammo * (maxAmmoPosY / maxAmmo));
        if (currentAmmoPosY >= 43)
        {
            currentAmmoPosY = 43;
        }
        chamber2.localScale = new Vector3(chamberScale.x, currentAmmoPosY, chamberScale.z);
        animator.Play(StaticData.ChamberBounce);
        // chamberTransparent.SetAmmoInChamberTransparent(oldAmmoPosY);
    }
    public void AddAmmoInChamber(float ammo)
    {
        // oldAmmoPosY = chamber.localScale.y;
        currentAmmoPosY = (ammo * (maxAmmoPosY / maxAmmo));
        chamber2.localScale = new Vector3(chamberScale.x, currentAmmoPosY, chamberScale.z);
        animator.Play(StaticData.ChamberUpBounce);
        // chamberTransparent.SetAmmoInChamberTransparent(oldAmmoPosY);
    }
}
