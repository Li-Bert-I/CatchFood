using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Laser laserPrefab;
    private Laser laser;
    private GameObject catched;
    private Vector3 oldPointOnWall = Vector3.zero;
    private Vector3 pointOnWall = Vector3.zero;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 p = Camera.main.ScreenToWorldPoint(
            new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.z) - 1.0f));
        pointOnWall = oldPointOnWall + Vector3.ClampMagnitude(p - oldPointOnWall, 20.0f * Time.deltaTime);
        Quaternion rotation  = Quaternion.LookRotation(transform.position - pointOnWall, Vector3.up);
        rotation.eulerAngles = new Vector3(rotation.eulerAngles.x + 90, rotation.eulerAngles.y, rotation.eulerAngles.z);
        transform.rotation = rotation;
        Vector3 laserPosition = transform.position + (pointOnWall - transform.position).normalized * laserPrefab.transform.localScale.y;
        if (Input.GetMouseButtonDown(0))
        {
            if (laser == null)
            {
                laser = Instantiate(laserPrefab, laserPosition, transform.rotation);
                laser.gun = this;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (laser != null)
            {
                Destroy(laser.gameObject);
                laser = null;
            }
            releaseCatched();
        }
        if (Input.GetMouseButton(0))
        {
            laser.transform.position = laserPosition;
            laser.transform.rotation = transform.rotation;
            if (catched != null)
            {
                catched.GetComponent<Rigidbody>().MovePosition(pointOnWall);
            }
        }
        oldPointOnWall = pointOnWall;
    }

    public void setCatched(GameObject other)
    {
        if (catched == null)
        {
            catched = other;
            catched.GetComponent<Rigidbody>().useGravity = false;
            catched.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void releaseCatched()
    {
        if (catched != null)
        {
            catched.GetComponent<Rigidbody>().useGravity = true;
            catched.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude((pointOnWall - oldPointOnWall) / Time.smoothDeltaTime, 5.0f);
            catched = null;
        }
    }
}
