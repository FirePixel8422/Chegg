using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Fire_Pixel.Networking
{
    public class AutoLoginHandler : MonoBehaviour
    {
        [SerializeField] private string mainSceneName = "Main Menu";
        private string loginSceneName;

        private AsyncOperation mainSceneLoadOperation;


        private void Start()
        {
            loginSceneName = SceneManager.CurrentSceneName;
            mainSceneLoadOperation = SceneManager.LoadSceneAsync(mainSceneName, LoadSceneMode.Additive, false);

            mainSceneLoadOperation.completed += (_) =>
            {
                SceneManager.UnLoadSceneAsync(loginSceneName);
            };

            _ = UnityServices.InitializeAsync();

            //AuthenticationService.Instance.SignOut();

            _ = AuthenticationService.Instance.SignInAnonymouslyAsync();

            //DebugLogger.Log("Logged into saved account!");
            //Debug.Log("Player ID: " + AuthenticationService.Instance.PlayerId);

            mainSceneLoadOperation.allowSceneActivation = true;
        }
    }
}