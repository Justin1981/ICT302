using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class MenuItemController : MonoBehaviour, IInputClickHandler {

    private TextMesh _textMesh;
    private IMenuItemData _menuItemData;

    public IMenuItemData MenuItemData
    {
        get { return _menuItemData; }
        set
        {
            if(_menuItemData == value)
            {
                return;
            }
            _menuItemData = value;
            _textMesh = GetComponentInChildren<TextMesh>();
            if(_menuItemData != null && _textMesh != null)
            {
                _textMesh.text = _menuItemData.Title;
            }
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if(MenuItemData != null)
        {
            PlayConfirmationSound();
        }
    }

    private AudioSource _audioSource;
    private void PlayConfirmationSound()
    {
        if(_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        if(_audioSource != null)
        {
            _audioSource.Play();
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
