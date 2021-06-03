//Author: Héctor Luis De Pablo; Source: Firebase
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    #region variables
    public DatabaseReference databaseReference;
    public FirebaseUser User;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        FirebaseDatabase firebaseDatabase = FirebaseDatabase.GetInstance("https://cursus-honorum-d2963-default-rtdb.europe-west1.firebasedatabase.app/");
        databaseReference = firebaseDatabase.RootReference;
    }
    private void SavePlayerFireBase(string mail, int highscore, string range, float time)
    {
        Player player = new Player(mail, highscore, range, time);
        string json = JsonUtility.ToJson(player);

        databaseReference.Child("Usuarios").Child(mail).SetRawJsonValueAsync(json);
    }

    private Player GetPlayerFromFireBase(string mail)
    {
        Player player=null; 
        FirebaseDatabase.GetInstance("https://cursus-honorum-d2963-default-rtdb.europe-west1.firebasedatabase.app/")
            .GetReference("Usuarios").Child(mail)
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


    
}
