//Author: Héctor Luis De Pablo
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizzDB : MonoBehaviour
{
    #region Fields
    [Header("FIELDS")]
    [SerializeField] private List<Question> questionList = null;
    #endregion
    //this is a variable for refill the list
    private List<Question> backup = null;

    public Question GetRandom(bool remove =true)
    {

        if (questionList.Count == 0)
        {
            //RestoreBackup();
            return null;
        }

        int index = Random.Range(0, questionList.Count);

            if (!remove)
            {
                return questionList[index];
            }

        Question question = questionList[index];
        questionList.RemoveAt(index);
            return question;
    }
    //This function will fill the list if it will be empty. In this project it is not necesary, but is useful.
    private void RestoreBackup()
    {
        questionList = backup.ToList();
    }

    //the database is saved in the backup. If we would need it we could restore it.
    private void Awake()
    {
        backup = questionList.ToList();
    }
}
