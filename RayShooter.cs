using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.DynamicGI;

public class RayShooter : MonoBehaviour
{
    private PlayerCharacter player;
    private Camera _camera;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 point;
    GameObject emitting;
    GameObject itemHeld;
    public GameObject guide;

    void Start()
    {
        _camera = GetComponent<Camera>();
        player = GetComponentInParent<PlayerCharacter>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (player.isAlive)
        {
            point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            ray = _camera.ScreenPointToRay(point);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;

                // turning off the emission (try because it will fail first)
                if (emitting != hitObject || Vector3.Distance(emitting.transform.position, transform.position) > 3.5 || player.holdingObj)
                {
                    try
                    {
                        emitting.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                    }
                    catch { }
                }

                if (Vector3.Distance(transform.position, hitObject.transform.position) < 3.5)
                {
                    //here i will check if the Interactable interface exists. and if it does, call it

                    try
                    {
                        if (hitObject.GetComponent<Rigidbody>().mass < .8)
                        {
                            //emission (highlights an objects when you can pick it up or interact with it)
                            if (!player.holdingObj)
                            {
                                emitting = hitObject;
                                hitObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                                //DynamicGI.SetEmissive(hitObject.GetComponent<Renderer>(), Color.green);
                                //hitObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);
                            }
                            if (Input.GetMouseButton(0))
                            {
                                Interactable interact = hitObject.GetComponent<Interactable>();
                                if (!player.holdingObj)
                                {
                                    if (interact != null)
                                        interact.Interact();
                                    else
                                    {
                                        itemHeld = hitObject.transform.gameObject;
                                        PickUpObj(itemHeld);
                                    }
                                }
                                //else
                                //{
                                //    if (hitObject.name == itemHeld.name)
                                //        DropObj(itemHeld);
                                //}
                            }
                            else 
                            {
                                if (player.holdingObj)
                                    DropObj(itemHeld);
                            }
                        }
                        //that means player is holding an object, so drop it!
                    }
                    catch
                    {

                    }

                }
            }
            if (player.holdingObj)
                PickUpObj(itemHeld);
        }
        //player died!
        else
        {
            if (player.holdingObj)
                DropObj(itemHeld);
        }
    }

    private void PickUpObj(GameObject hitObject)
    {
        player.holdingObj = true;
        hitObject.GetComponent<Rigidbody>().isKinematic = true;
        hitObject.GetComponent<Rigidbody>().useGravity = false;
        hitObject.GetComponent<Rigidbody>().MovePosition(guide.transform.position);
        hitObject.GetComponent<Rigidbody>().MoveRotation(guide.transform.rotation);

        //hitObject.transform.position = guide.GetComponent<Transform>().position;
        //hitObject.transform.rotation = guide.GetComponent<Transform>().rotation;
    }
    private void DropObj(GameObject hitObject)
    {
        player.holdingObj = false;
        hitObject.GetComponent<Rigidbody>().isKinematic = false;
        hitObject.GetComponent<Rigidbody>().useGravity = true;
    }
    IEnumerator ShowMessage(string message)
    {
        //showingMessage = true;

        //        alertedText.GetComponent<Text>().text = message;
        //        for (float i = 0; i < 1; i += 0.02f)
        //        {
        //            alertedText.GetComponent<CanvasGroup>().alpha = i;
        //            yield return new WaitForEndOfFrame();
        //        }
        //        yield return new WaitForSeconds(5.0f);
        //        for (float i = 1; i > 0; i -= 0.02f)
        //        {
        //            alertedText.GetComponent<CanvasGroup>().alpha = i;
        //            yield return new WaitForEndOfFrame();
        //        }
        yield return new WaitForSeconds(2);

        //showingMessage = false;

    }
}
