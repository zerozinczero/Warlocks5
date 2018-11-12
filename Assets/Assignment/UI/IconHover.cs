using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconHover : MonoBehaviour
{


    [SerializeField] private Text hoverText;
    [SerializeField] private string description;
    //[SerializeField] private bool textStatus;
    [SerializeField] private GameObject TextBox;

    // Use this for initialization


    private void OnMouseEnter()
    {
        TextBox.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, 0f);
        hoverText.text = description;
        TextBox.SetActive(true);
    }

    private void OnMouseExit()
    {
        TextBox.SetActive(false);

    }


	// Update is called once per frame
	void Update () {
		
	}
}
