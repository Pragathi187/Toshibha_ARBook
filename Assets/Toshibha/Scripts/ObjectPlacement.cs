using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Windows;

public class ObjectPlacement : MonoBehaviour
{
    // Start is called before the first frame update

    public XROrigin xrSessionOrigin;
    public List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
    public GameObject canvasMenu;


    public GameObject objectSpwaned;

    Pose placementpose;
    public GameObject[] itemToSpawn;
    public GameObject[] buttons;
    public TMP_Text instruction_text;
    public Image iconImage;

    public Sprite scanSurfaceSprite;
    public Sprite tapToPlaceSprite;

    int objIndex;

    bool isDetecting = true;
    bool isSurfaceTracked = false;

    void Start()
    {
        iconImage.sprite = scanSurfaceSprite;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInstructionText();
        TrackInput();


    }
    void TrackInput()
    {
        Vector2 pos;
#if UNITY_EDITOR
        pos = UnityEngine.Input.mousePosition;
#elif PLATFORM_ANDROID
        pos = UnityEngine.Input.GetTouch(0).position;
#endif
        if ((UnityEngine.Input.touchCount > 0 || UnityEngine.Input.GetMouseButtonDown(0)) && !IsPointerOverUIObject(pos))
        {

            bool isHit = xrSessionOrigin.GetComponent<ARRaycastManager>().Raycast(pos, raycastHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);

            if (isHit)
            {
                instruction_text.text = "";
                if (objectSpwaned == null)
                {

                    PlaceObject();
                    isDetecting = false;
                    canvasMenu.gameObject.SetActive(true);

                    foreach (var plane in xrSessionOrigin.GetComponent<ARPlaneManager>().trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }

                    xrSessionOrigin.GetComponent<ARPlaneManager>().enabled = false;
                }

                objectSpwaned.transform.position = raycastHits[0].pose.position;
                placementpose = raycastHits[0].pose;
            }
        }
    }
    void UpdateInstructionText()
    {
        if (!isSurfaceTracked)
        {
            if (xrSessionOrigin.GetComponent<ARPlaneManager>().trackables.count > 0)
            {
                instruction_text.text = "Tap on the tracked surface to place object";
                iconImage.sprite = tapToPlaceSprite;
                isSurfaceTracked = true;
            }
        }



    }
    public void PlaceObject()
    {
        if (isDetecting)
        {
            objIndex = 0;


        }
        else
        {
            string buttonName = EventSystem.current.currentSelectedGameObject.name;
            for (int i = 0; i < buttons.Length; i++)
            {

                if (buttons[i].name == buttonName)
                {

                    objIndex = i;
                }
            }
        }

        UpdateButtonOutline();
        if (objectSpwaned != null) Destroy(objectSpwaned);
        objectSpwaned = Instantiate(itemToSpawn[objIndex]);
        objectSpwaned.transform.position = placementpose.position;
        iconImage.enabled = false;
    }
    //private bool IsPointerOverUIObject()
    //{
    //    //PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    //    //eventDataCurrentPosition.position= new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //    //List<RaycastResult> results = new List<RaycastResult>();
    //    //EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
    //    return results.Count > 0;

    //}

    private void UpdateButtonOutline()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Outline>().enabled = false;

        }

        buttons[objIndex].GetComponent<Outline>().enabled = true;
    }


    public void Reset()
    {
        isDetecting = true;

        if (objectSpwaned != null) Destroy(objectSpwaned);
        xrSessionOrigin.GetComponent<ARPlaneManager>().enabled = true;
        instruction_text.text = "Move the Camera to scan the surface";
        isSurfaceTracked = false;
    }

    public void Toggle()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(!buttons[i].activeInHierarchy);
        }

    }

    private bool IsPointerOverUIObject(Vector2 touchPosition)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = touchPosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;

    }
}
