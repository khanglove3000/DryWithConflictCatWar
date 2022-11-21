using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_ZeusRangeGetAttack : MonoBehaviour
{
    public List<Cat_Controller> listCatControllers = null;

    public Cat_Controller catController;

    private void Update()
    {
        checkListCatTarget();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag != "Cat") return;
        Cat_Controller _cat = collision.gameObject.GetComponent<Cat_Controller>();
        if (catController.catType != _cat.catType)
        {
            listCatControllers.Add(_cat);
        }
      
    }
    public void checkListCatTarget()
    {
        if (listCatControllers.Count <= 0) return; 

        for(int i = 0; i < listCatControllers.Count; i++)
        {
            if(listCatControllers[i] == null)
            {
                listCatControllers.RemoveAt(i);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < listCatControllers.Count; i++)
                listCatControllers.RemoveAt(i);
    }

}
