using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : MonoBehaviour
{
    //Universal Cube
    public static int CubeUpAnimation = Animator.StringToHash("CubeUpAnimation");
    public static int PlayerForwardFly = Animator.StringToHash("PlayerForwardFly");

    //Universal Cube DestroyAnimator1
    public static int Explode1 = Animator.StringToHash("Explode1");
    public static int Explode2 = Animator.StringToHash("Explode2");
    public static int Explode3 = Animator.StringToHash("Explode3");
    public static int Explode4 = Animator.StringToHash("Explode4");
    public static int Explode5 = Animator.StringToHash("Explode5");
    public static int Explode6 = Animator.StringToHash("Explode6");
    public static int Boom1 = Animator.StringToHash("Boom1");
    public static int Boom2 = Animator.StringToHash("Boom2");
    public static int Boom3 = Animator.StringToHash("Boom3");
    public static int Boom4 = Animator.StringToHash("Boom4");
    public static int Boom5 = Animator.StringToHash("Boom5");

    //Combo
    public static int ComboAnimation = Animator.StringToHash("ComboAnimation");
    public static int AddPointsAnimation = Animator.StringToHash("AddPointsAnimation");

    //Chamber
    public static int ChamberBounce = Animator.StringToHash("ChamberBounce");
    public static int ChamberUpBounce = Animator.StringToHash("ChamberUpBounce");

    //ChamberTransparent
    public static int ChamberTransparentGoDown = Animator.StringToHash("ChamberTransparentGoDown");

    //Chetchik
    public static int MoveToDoubleChetchick = Animator.StringToHash("MoveToDoubleChetchick");
    public static int MoveToStartChetchick = Animator.StringToHash("MoveToStartChetchick");

    //Gun
    public static int GunAnimator = Animator.StringToHash("GunAnimator");
    public static int GunBreaksDown = Animator.StringToHash("GunBreaksDown");
    public static int CreateSecondGun = Animator.StringToHash("CreateSecondGun");
    public static int DestroySecondGun = Animator.StringToHash("DestroySecondGun");

    //Pivot
    public static int BuffAnimation = Animator.StringToHash("BuffAnimation");
    public static int HorizontalBoom = Animator.StringToHash("HorizontalBoom");
    public static int VerticalBoom = Animator.StringToHash("VerticalBoom");

}
