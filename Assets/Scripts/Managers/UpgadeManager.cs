using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgadeManager : MonoBehaviour
{
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            Tower tower = hitObject.GetComponent<Tower>();
            if (tower && tower.gameObject.GetComponentInChildren<SimpleTowerAttack>().enabled == true && Input.GetMouseButtonDown(0))
            {
                tower.Button.SetActive(!tower.Button.GetComponent<Button>().IsActive());
                
            }
        }
    }
}
