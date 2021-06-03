//Author: Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class QuizzUI : MonoBehaviour
{
    #region Fields
    [Header("FIELDS")]
    [SerializeField] private Text quizzQuestion = null;
    [SerializeField] private List<OptionsButton> quizzButtonList = null;
    #endregion
    public void InitQuizzUI(Question question, Action<OptionsButton> callback)
    {
        if (quizzQuestion != null)
        {
            quizzQuestion.text = question.text;

            for (int i = 0; i < quizzButtonList.Count; i++)
            {
            
                quizzButtonList[i].InitOptionsButton(question.options[i], callback);
            }
        }
                
    }

    
}
