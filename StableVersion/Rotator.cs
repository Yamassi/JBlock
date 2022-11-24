using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    Transform rotator;
    Rotator rotatorComponent;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 vectorRotation;
    [SerializeField] private float bounceTime;
    private Animator animator;
    [SerializeField] private bool isPlayingAnimation = true;
    private bool isDestroyTimerOn = false;
    private bool isRotateOn = true;
    private FireWorksFX3 fireWorksFX3;
    private FireWorksFX4 fireWorksFX4;
    private ParticleSystem fireWorksFX3particle;
    private ParticleSystem fireWorksFX4particle;
    [SerializeField] private bool isFireWorks;
    void Start()
    {
        rotator = transform;
        animator = GetComponent<Animator>();
        rotatorComponent = GetComponent<Rotator>();
        if (rotator.position.y >= 15)
        {
            rotatorComponent.enabled = false;
        }
        if (isFireWorks)
        {
            fireWorksFX3 = rotator.GetComponentInChildren<FireWorksFX3>(true);
            fireWorksFX4 = rotator.GetComponentInChildren<FireWorksFX4>(true);
            fireWorksFX3particle = fireWorksFX3.GetComponent<ParticleSystem>();
            fireWorksFX4particle = fireWorksFX4.GetComponent<ParticleSystem>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isRotateOn)
        {
            transform.Rotate(vectorRotation * speed * Time.deltaTime);
            if (isPlayingAnimation)
            {
                // print("start");
                StartCoroutine(AnimationWaitForSeconds(bounceTime));
            }
        }




        if (isDestroyTimerOn)
        {
            if (isFireWorks)
            {
                StartCoroutine(DestroyRotatorWithFireWorks());
            }
            else if (!isFireWorks)
            {
                StartCoroutine(DestroyRotator());
            }

        }
    }
    private IEnumerator AnimationWaitForSeconds(float seconds)
    {
        // Do something before
        animator.enabled = true;
        animator.Play(StaticData.BuffAnimation);
        isPlayingAnimation = false;
        yield return new WaitForSeconds(seconds);
        isPlayingAnimation = true;

        // Do something after
    }
    public void TimerOn()
    {
        isDestroyTimerOn = true;
    }
    public void RotateOff()
    {
        isRotateOn = false;
    }
    IEnumerator DestroyRotator()
    {

        yield return new WaitForSeconds(0.42f);
        Destroy(rotator.gameObject);
    }
    IEnumerator DestroyRotatorWithFireWorks()
    {
        fireWorksFX3.gameObject.SetActive(true);
        fireWorksFX4.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.42f);
        fireWorksFX3.gameObject.transform.parent = null;
        fireWorksFX4.gameObject.transform.parent = null;
        fireWorksFX3particle.Play();
        fireWorksFX4particle.Play();
        Destroy(rotator.gameObject);
    }
}
