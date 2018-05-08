using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var builder = GetComponent<MenuBuilder>();

        IList<IMenuItemData> list = new List<IMenuItemData>();

        list.Add(new MenuItemData
        {
            Title = "Test1 Button",
            MenuId = 1,
            SelectMessageObject = null
        });

        list.Add(new MenuItemData
        {
            Title = "Test2 Button",
            MenuId = 2,
            SelectMessageObject = null
        });

        builder.MenuItems = list;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
