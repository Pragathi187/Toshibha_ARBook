using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class InteractionHandler : MonoBehaviour
{

    ARScaleInteractable aRScaleInteractable;
    ARRotationInteractable aRRotationInteractable;
    ARTranslationInteractable aRTranslationInteractable;

    private void Awake()
    {
        aRScaleInteractable = GetComponent<ARScaleInteractable>();
        aRRotationInteractable = GetComponent<ARRotationInteractable>();
        aRTranslationInteractable = GetComponent<ARTranslationInteractable>();
    }

    void OnEnable()
    {
        SceneManager.moveBtnAction += onMoveBtnPressed;
        SceneManager.rotationBtnAction += onRotationBtnPressed;
        SceneManager.scaleBtnAction += onScaleBtnPressed;
    }

    private void OnDisable()
    {
        SceneManager.moveBtnAction -= onMoveBtnPressed;
        SceneManager.rotationBtnAction -= onRotationBtnPressed;
        SceneManager.scaleBtnAction -= onScaleBtnPressed;
    }

    void Start()
    {
        aRScaleInteractable.enabled = false;
        aRRotationInteractable.enabled = false;
        aRTranslationInteractable.enabled = false;
    }

    void onMoveBtnPressed()
    {
        aRScaleInteractable.enabled = false;
        aRRotationInteractable.enabled = false;
        aRTranslationInteractable.enabled = true;
    }

    void onScaleBtnPressed()
    {
        aRScaleInteractable.enabled = true;
        aRRotationInteractable.enabled = false;
        aRTranslationInteractable.enabled = false;
    }

    void onRotationBtnPressed()
    {
        aRScaleInteractable.enabled = false;
        aRRotationInteractable.enabled = true;
        aRTranslationInteractable.enabled = false;
    }

}
