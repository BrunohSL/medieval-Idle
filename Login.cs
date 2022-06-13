using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class Login : MonoBehaviour {
    private string authenticationEndpoint = "http://localhost:3000/";
    [SerializeField] private TMP_InputField _userInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private TMP_Text _alertText;
    [SerializeField] private Button _loginButton;

    public void loginButton() {
        _alertText.text = "Signing in...";
        _loginButton.interactable = false;

        StartCoroutine(tryLogin());
    }

    private IEnumerator tryLogin() {
        string username = _userInput.text;
        string password = _passwordInput.text;

        // string test = "{\"username\":\"teste\"}";
        // GameAccount gameAccount = JsonUtility.FromJson<GameAccount>(test);
        // Debug.Log(gameAccount._id);
        // Debug.Log(gameAccount.username);

        string jsonBody = JsonUtility.ToJson(new GameAccount(username, password));

        UnityWebRequest request = UnityWebRequest.Post(authenticationEndpoint + "login", "POST");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonBody));
        request.SetRequestHeader("Content-Type", "application/json");
        var handler = request.SendWebRequest();

        float timer = 0.0f;
        while (!handler.isDone) {
            timer += Time.deltaTime;

            if (timer > 10.0f) {
                _alertText.text = "Server timed out";
                _loginButton.interactable = true;
                yield break;
            }

            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success) {
            Debug.Log(request.downloadHandler.text);
            GameAccount gameAccount = JsonUtility.FromJson<GameAccount>(request.downloadHandler.text);
            Debug.Log(gameAccount._id);
            Debug.Log(gameAccount.username);
            Debug.Log(gameAccount.password);

            _loginButton.interactable = true;
        } else {
            _alertText.text = request.downloadHandler.text != "" ? request.downloadHandler.text : "Server timed out";
            _loginButton.interactable = true;
        }

        yield return null;
    }

    private IEnumerator trySignup() {
        string username = _userInput.text;
        string password = _passwordInput.text;

        string jsonBody = JsonUtility.ToJson(new GameAccount(username, password));

        UnityWebRequest request = UnityWebRequest.Post(authenticationEndpoint + "signup", "POST");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonBody));
        request.SetRequestHeader("Content-Type", "application/json");
        var handler = request.SendWebRequest();

        float timer = 0.0f;
        while (!handler.isDone) {
            timer += Time.deltaTime;

            if (timer > 10.0f) {
                _alertText.text = "Server timed out";
                _loginButton.interactable = true;
                break;
            }

            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success) {
            Debug.Log(request.downloadHandler.text);
            _alertText.text = "Cadastrado com sucesso";
            _loginButton.interactable = true;
        } else {
            _alertText.text = request.downloadHandler.text;
            _loginButton.interactable = true;
        }

        yield return null;
    }
}

[SerializeField]
public class GameAccount {
    public string _id;
    public string username;
    public string password;

    public GameAccount(string username, string password) {
        this.username = username;
        this.password = password;
    }

    public GameAccount(string id, string username, string password) {
        this._id = id;
        this.username = username;
        this.password = password;
    }
}
