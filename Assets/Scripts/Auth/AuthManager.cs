//Author:Héctor Luis De Pablo Source: Firebase
using System.Collections;
using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase;
using Firebase.Database;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    #region Firebase Fields
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference databaseReference;
    #endregion
    #region Player Fields
    [Header("Player")]
    public GameObject playerGO;
    public Player player;
    public string tagName = "player";
    #endregion
    #region Login & Register Fields
    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;
    #endregion

    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        FirebaseDatabase firebaseDatabase = FirebaseDatabase.GetInstance("https://cursus-honorum-d2963-default-rtdb.europe-west1.firebasedatabase.app/");
        databaseReference = firebaseDatabase.RootReference;
        #region Firebase Player
        //playerGO = GameObject.FindGameObjectWithTag(tagName);
        //player = playerGO.GetComponent<Player>();
        #endregion
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    private IEnumerator Login(string email, string password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            //if there are specific errors, the system will launch a specific message. If there aren't it will launch the standard message
            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //player = new Player();
            //User is now logged in
            //it recovers the data from the DB
            //player = GetPlayerFromFireBase(_email);
            #region PlayerPrefs
            //we save the data in the PlaterPrefs
            PlayerPrefs.SetString("mail", emailLoginField.text);
            PlayerPrefs.SetInt("highscore", 0);
            PlayerPrefs.SetString("range", "Duunviro");
            PlayerPrefs.SetFloat("time", 0.0f);
            
            //we save the info in our system
            //player.mail = PlayerPrefs.GetString("mail");
            //player.highscore = PlayerPrefs.GetInt("highscore");
            //player.range = PlayerPrefs.GetString("range");
            //player.time = PlayerPrefs.GetFloat("time");
            #endregion
            
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";
            //it goes to the next scene
            SceneManager.LoadScene("InitialCredits");
            
           
        }
    }

    private IEnumerator Register(string email, string password, string username)
    {
        if (username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //now save thye info about the player in FireBase DB
                        //SavePlayerFireBase(emailRegisterField.text);
                        //Username is now set
                        //Now return to login screen
                        //AuthUIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                    }
                }
            }
        }
    }
    #region DB methods
    private Player GetPlayerFromFireBase(string mail)
    {
        Player player = null;
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
                        player = new Player(mail, playerDevuelto.highscore, playerDevuelto.range, playerDevuelto.time);

                    }

                }
                return player;
            });

        return player;

    }

    private void SavePlayerFireBase(string mail)
    {
        Player player = new Player(mail, 0, "Duunviro", 0);
        string json = JsonUtility.ToJson(player);

        databaseReference.Child("cursus-honorum-d2963-default-rtdb").SetRawJsonValueAsync(json);
    }
    #endregion


}
