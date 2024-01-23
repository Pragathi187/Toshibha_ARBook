using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class InteractionHandler : MonoBehaviour
{

    public ARScaleInteractable aRScaleInteractable;
    public ARRotationInteractable aRRotationInteractable;
    public ARTranslationInteractable aRTranslationInteractable;

    private void Awake()
    {
        // aRScaleInteractable = GetComponent<ARScaleInteractable>();
        // aRRotationInteractable = GetComponent<ARRotationInteractable>();
        // aRTranslationInteractable = GetComponent<ARTranslationInteractable>();
    }

    void OnEnable()
    {
        // SceneManager.moveBtnAction += onMoveBtnPressed;
        // SceneManager.rotationBtnAction += onRotationBtnPressed;
        // SceneManager.scaleBtnAction += onScaleBtnPressed;
    }

    private void OnDisable()
    {
        // SceneManager.moveBtnAction -= onMoveBtnPressed;
        // SceneManager.rotationBtnAction -= onRotationBtnPressed;
        // SceneManager.scaleBtnAction -= onScaleBtnPressed;
    }

    void Start()
    {
        // aRScaleInteractable.enabled = false;
        // aRRotationInteractable.enabled = false;
        // aRTranslationInteractable.enabled = false;
    }

    void onMoveBtnPressed()
    {
        Debug.Log("Move Selected");
        aRScaleInteractable.enabled = false;
        aRRotationInteractable.enabled = false;
        aRTranslationInteractable.enabled = true;
    }

    void onScaleBtnPressed()
    {
        aRScaleInteractable.enabled = true;
        aRRotationInteractable.enabled = false;
        aRTranslationInteractable.enabled = false;
        Debug.Log("Scale Selected");
    }

    void onRotationBtnPressed()
    {
        aRScaleInteractable.enabled = false;
        aRRotationInteractable.enabled = true;
        aRTranslationInteractable.enabled = false;
        Debug.Log("Rotation Selected");
    }

}
