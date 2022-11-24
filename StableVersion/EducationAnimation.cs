using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EducationAnimation : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    public void StartEdu()
    {
        animator.Play("Level1Education");
    }
}
