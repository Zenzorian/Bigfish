using Scripts.Logic;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Infrastructure
{
    public class SceneLoaderService : ISceneLoaderService
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly LoadingCurtain _curtain;

        public SceneLoaderService(ICoroutineRunner coroutineRunner, LoadingCurtain coroutine)
        {
            _coroutineRunner = coroutineRunner;
            _curtain = coroutine;            
        }
        public void ShowLoadingCurtain()
        {
            _curtain.Show();
        }

        public void Load(string name, Action onLoaded = null) =>
          _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                _curtain.Show();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;

           
            onLoaded?.Invoke();
            
            _curtain.Hide();
        }
    }
}