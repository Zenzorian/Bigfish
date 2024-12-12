using System;
using System.Collections;

namespace Scripts.Infrastructure
{
    public interface ISceneLoaderService
    {
        void Load(string name, Action onLoaded = null);
        void ShowLoadingCurtain();
    }
}