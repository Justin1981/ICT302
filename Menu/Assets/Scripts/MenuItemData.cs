using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemData : IMenuItemData {

	public object SelectMessageObject { get; set; }
    public string Title { get; set; }
    public int MenuId { get; set; }
}
