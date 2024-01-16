using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectPlacement : MonoBehaviour
{
    // Start is called before the first frame update

    public XROrigin xrSessionOrigin;
    public List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
    public GameObject canvasMenu;
   
    
    GameObject objectSpwaned;
   
    Pose placementpose;
    public GameObject[] itemToSpawn;
    public GameObject[] buttons;

    int objIndex;

    public bool isDetecting = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Detect user Touch
        //Project a Raycast
        //Instantiate Virtual object at point where ray interacts world
        if(Input.GetMouseButton(0))
        {
            Debug.Log("MouseClick");
            bool isHit = xrSessionOrigin.GetComponent<ARRaycastManager>().Raycast(Input.mousePosition, raycastHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);

            if(isHit && !IsPointerOverUIObject())
            {
                if(objectSpwaned==null)
                {
                   
                    PlaceObject();
                    isDetecting = false;
                    canvasMenu.gameObject.SetActive(true);


                    foreach (var plane in xrSessionOrigin.GetComponent<ARPlaneManager>().trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }

                    //xrSessionOrigin.GetComponent<ARPlaneManager>().enabled = false;
                }

                objectSpwaned.transform.position = raycastHits[0].pose.position;
                placementpose = raycastHits[0].pose;
            }
        }
    }


    //public void ChangeProduct(GameObject product)
    //{
    //    Destroy(objectSpwaned);
    //    objectSpwaned = Instantiate(product);
    //    objectSpwaned.transform.position=placementpose.position;

    //}   

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
        
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position= new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;

    }

    private void UpdateButtonOutline()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Outline>().enabled = false;

        }

        buttons[objIndex].GetComponent<Outline>().enabled = true;
    }
}
