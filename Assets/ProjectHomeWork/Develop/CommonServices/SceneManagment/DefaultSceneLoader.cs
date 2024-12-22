using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.ProjectHomeWork.Develop.CommonServices.SceneManagment
{
    public class DefaultSceneLoader : ISceneLoader
    {
        public IEnumerator LoadAsync(SceneID sceneID, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            AsyncOperation waitLoading = SceneManager.LoadSceneAsync(sceneID.ToString(), loadSceneMode);

            while(waitLoading.isDone == false)
                yield return null;
        } 
    }
}
