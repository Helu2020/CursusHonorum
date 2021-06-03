//Author: Héctor Luis De Pablo
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class represents the buttons that the player can choose
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class OptionsButton : MonoBehaviour
{
    #region variables
    private Text quizzText = null;
    private Button quizzButton = null;
    private Image quizzImage = null;
    private Color quizzOriginalColor = Color.cyan;
    public Option option { get; set; }
    #endregion
    #region Constructor
    //the callback notifies the player's choose
    public OptionsButton(Option option, Action<OptionsButton> callback)
    {
        quizzText.text = option.text;
        quizzButton.enabled = true;
        quizzImage.color = quizzOriginalColor;
        this.option = option; 
        //cuando seleccionemos esta opción se lo enviamos para su revisión
        quizzButton.onClick.AddListener(delegate
        {
            callback(this);
        });

    }
    #endregion
    //the callback notifies the player's choose
    public void InitOptionsButton(Option option, Action<OptionsButton> callback)
    {
        quizzText.text = option.text;
        quizzButton.onClick.RemoveAllListeners();
        quizzButton.enabled = true;
        quizzImage.color = quizzOriginalColor;
        this.option = option;
        //When this option is selected it will be sended for revision
        quizzButton.onClick.AddListener(delegate
        {
            callback(this);
        });
    }

    public void Awake()
    {
        quizzButton = GetComponent<Button>();
        quizzImage = GetComponent<Image>();
        quizzText = transform.GetChild(0).GetComponent<Text>();

        quizzOriginalColor = quizzImage.color;
    }

    public void SetColor(Color color)
    {
        quizzButton.enabled = false;
        quizzImage.color = color;
    }
}
