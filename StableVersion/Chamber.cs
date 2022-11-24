using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamber : MonoBehaviour
{
    private float maxAmmoPosY = 43;
    private float oldAmmoPosY;
    private float currentAmmoPosY;
    private float ammoInChamber;
    private Vector3 chamberScale;
    [SerializeField] float maxAmmo;
    Transform chamber;
    Animator animator;
    // ChamberTransparent chamberTransparent;
    private void Start()
    {
        chamber = transform;
        chamberScale = new Vector3(chamber.localScale.x, maxAmmoPosY, chamber.localScale.z);
        animator = chamber.GetComponent<Animator>();
        if (currentAmmoPosY >= maxAmmoPosY)
        {
            currentAmmoPosY = maxAmmoPosY;
            chamber.localScale = new Vector3(chamberScale.x, currentAmmoPosY, chamberScale.z);
        }
        // chamberTransparent = FindObjectOfType<ChamberTransparent>();
    }
    public void SetAmmoInChamber(float ammo)
    {
        // oldAmmoPosY = chamber.localScale.y;
        currentAmmoPosY = (ammo * (maxAmmoPosY / maxAmmo));
        if (currentAmmoPosY >= maxAmmoPosY)
        {
            currentAmmoPosY = maxAmmoPosY;
        }
        chamber.localScale = new Vector3(chamberScale.x, currentAmmoPosY, chamberScale.z);
        animator.Play(StaticData.ChamberBounce);
        // chamberTransparent.SetAmmoInChamberTransparent(oldAmmoPosY);
    }
    public void AddAmmoInChamber(float ammo)
    {

        // oldAmmoPosY = chamber.localScale.y;
        currentAmmoPosY = (ammo * (maxAmmoPosY / maxAmmo));
        if (currentAmmoPosY >= maxAmmoPosY)
        {
            currentAmmoPosY = maxAmmoPosY;
        }
        chamber.localScale = new Vector3(chamberScale.x, currentAmmoPosY, chamberScale.z);

        animator.Play(StaticData.ChamberUpBounce);

        // chamberTransparent.SetAmmoInChamberTransparent(oldAmmoPosY);
    }
}
