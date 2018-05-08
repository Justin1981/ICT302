using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMenuItemData
{
    object SelectMessageObject { get; set; }
    string Title { get; set; }
    int MenuId { get; set; }
}
