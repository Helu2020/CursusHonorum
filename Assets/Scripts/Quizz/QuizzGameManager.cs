//Author:Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using Firebase.Database;
using Firebase.Auth;


[RequireComponent(typeof(AudioSource))]
public class QuizzGameManager : MonoBehaviour
{

    #region serializeField
    [Header("FIELDS")]
    [SerializeField]private AudioClip correctSound = null;
    [SerializeField] private AudioClip incorrectSound = null;
    [SerializeField] private Color correctColor = Color.black;
    [SerializeField] private Color incorrectColor = Color.black;
    [SerializeField] private float waitTime = 0.0f;
    [SerializeField] private TextMeshProUGUI range = null;
    [SerializeField] private Text highScore = null;
    #endregion

    #region variables
    public DatabaseReference databaseReference;
    public FirebaseUser User;

    private QuizzDB quizzDB = null;
    private QuizzUI quizzUI = null;
    private AudioSource audioSource = null;
    private int score =0;
    private readonly int previousHighscore = 0;
    private readonly string previousRange = "";
    private int questionCount = 0;

    private Player player = new Player();
    #endregion


    //we use start because it is goes after than awake
    private void Start()
    {
        //It was for the firebase basedata functions. Finally, it doesn't work
        #region Firebase
        FirebaseDatabase firebaseDatabase = FirebaseDatabase.GetInstance("https://cursus-honorum-d2963-default-rtdb.europe-west1.firebasedatabase.app/");
        databaseReference = firebaseDatabase.RootReference;
        quizzDB = GameObject.FindObjectOfType<QuizzDB>();
        quizzUI = GameObject.FindObjectOfType<QuizzUI>();
        player = GetPlayerFromFireBase();
        #endregion

        audioSource = GetComponent<AudioSource>();

        NextQuestion();
    }

    //It builds the interface for a new question
    private void NextQuestion()
    {
            quizzUI.InitQuizzUI(quizzDB.GetRandom(), GiveAnswer);
            questionCount++;
    }

    //It initializes the routine when a player selects an option
    private void GiveAnswer(OptionsButton optionButton)
    {
        StartCoroutine(GiveAnswerRoutine(optionButton));
    }

    //When a question is selected the correct-wrong sound is sound and it waits a few seconds before the new question appears
    private IEnumerator GiveAnswerRoutine(OptionsButton optionButton)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        //ternary operator: if the solution is true it will take back the first; if it is not, it twill take back the second. We do this for color and sound.
        audioSource.clip = optionButton.option.isCorrect ? correctSound : incorrectSound;
        optionButton.SetColor(optionButton.option.isCorrect ? correctColor : incorrectColor);

        audioSource.Play();

        //We wait until the changes have been refreshed
        yield return new WaitForSeconds(waitTime);
     

        if (optionButton.option.isCorrect)
        {
           //We refresh the range every time the player chooses a correct answer
            score += 10;
            highScore.text = "Highscore: " + score.ToString();
            range.text = "Rango: " + FindRange();
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.SetString("range", range.text);

        }
        //Next question. We fixed the list of questions in 10.
        if (questionCount<10)
        {
            NextQuestion();
            
        }
        else
        {
            GameOver();
            
        }
        
    }

    private void GameOver()
    {

        range.text = FindRange();
        //Save PlayerPrefs
        PlayerPrefs.SetInt("highscore", score);
        PlayerPrefs.SetString("range", range.text);

        //We save the player data in our System
        player = new Player(PlayerPrefs.GetString("mail"), score, range.text, PlayerPrefs.GetFloat("time"));
        SaveSystem.SavePlayer(player);
        
        StartCoroutine(WaitRoutine());
        SceneManager.LoadScene("FinalCredits");
    }

    IEnumerator WaitRoutine()
    {
      yield return new WaitForSeconds(waitTime);
    }

    #region auxiliars functions

    private string FindRange()
    {
        
        if (score > previousHighscore)
        {
            return CalculateRange(score);
        }
        else
        {
            return previousRange;
        }
    }

    private string CalculateRange(int compareVar)
    {
        string range = "";

        if (compareVar < 20)
        {
            range = "Duunviro";
        } 
        else if(compareVar >= 20 && compareVar < 40)
        {
            range = "Tribuno";
        } 
        else if (compareVar >= 40 && compareVar < 60)
        {
            range = "Cuestor";
        } 
        else if (compareVar >= 60 && compareVar < 80)
        {
            range = "Pretor";
        }
        else if (compareVar >= 80 && compareVar < 100)
        {
            range = "Propretor";
        }
        else if (compareVar == 100)
        {
            range = "Consul";
        }

        return range;

    }

    //Firebase methods
    #region Firebase
    private Player GetPlayerFromFireBase()
    {
        Player player = null;
        FirebaseDatabase.GetInstance("https://cursus-honorum-d2963-default-rtdb.europe-west1.firebasedatabase.app/")
            .GetReference("Usuarios")
            .GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    // Handle the error...
                    Debug.LogError("Error in 34 line in DBManager in GetPlayerFromFireBase");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    foreach (var usuario in snapshot.Children)
                    {
                        Player playerDevuelto = JsonUtility.FromJson<Player>(usuario.GetRawJsonValue());
                        player = new Player(playerDevuelto.mail, playerDevuelto.highscore, playerDevuelto.range, playerDevuelto.time);

                    }

                }
                return player;
            });

        return player;

    }

    private void SavePlayerFireBase(string mail, int highscore, string range, float time)
    {
        Player player = new Player(mail, highscore, range, time);
        string json = JsonUtility.ToJson(player);

        databaseReference.Child("Usuarios").Child(mail).SetRawJsonValueAsync(json);
    }
    #endregion
    #endregion


}
