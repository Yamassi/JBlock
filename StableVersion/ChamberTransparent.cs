using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChamberTransparent : MonoBehaviour
{
    private float ammoInChamber;
    private Vector3 chamberScale;
    [SerializeField] float maxAmmo;
    Transform chamber;
    Animator animator;
    private void Start()
    {
        chamber = transform;
        chamberScale = new Vector3(chamber.localScale.x, 0, chamber.localScale.z);
        animator = chamber.GetComponent<Animator>();
    }
    public void SetAmmoInChamberTransparent(float oldAmmoPosY)
    {
        chamber.localScale = new Vector3(chamberScale.x, oldAmmoPosY, chamberScale.z);
        animator.Play(StaticData.ChamberTransparentGoDown);
    }
}
