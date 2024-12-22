using Assets.ProjectHomeWork.Develop.CommonServices.AssetsManagment;
using Assets.ProjectHomeWork.Develop.CommonServices.CoroutinePerformer;
using Assets.ProjectHomeWork.Develop.CommonServices.LoadingScreen;
using Assets.ProjectHomeWork.Develop.DI;
using System;
using UnityEngine;

namespace Assets.ProjectHomeWork.Develop.EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Bootstrap _gameBootstrap;
        private void Awake()
        {
            SetupAppSettings();

            DIContainer projectContainer = new DIContainer();

            //регистрация сервисов на целый проект
            //аналог global context из популярных DI фреймворках
            //родительский контейнер для всех будущих

            //projectContainer.RegisterAsSingle<ResourcesAssetLoader>(c => new ResourcesAssetLoader());
            //projectContainer.RegisterAsSingle(c => new ResourcesAssetLoader());
            RegisterResourcesAssetLoader(projectContainer);
            RegisterCoroutinePerformer(projectContainer);
            RegisterSceneLoader(projectContainer);

            //Все регистрации прошли
            projectContainer.Resolve<ICoroutinePerformer>().StartPerform(_gameBootstrap.Run(projectContainer));
        }


        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private void RegisterResourcesAssetLoader(DIContainer container)
            => container.RegisterAsSingle(c => new ResourcesAssetLoader());

        private void RegisterCoroutinePerformer(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutinePerformer>(c =>
            {
                ResourcesAssetLoader resourcesAssetLoader = c.Resolve<ResourcesAssetLoader>();

                CoroutinePerformer coroutinePerformerPrefab = resourcesAssetLoader.LoadResource<CoroutinePerformer>(InfrastructureAssetPaths.CoroutinePerformerPath);

                return Instantiate(coroutinePerformerPrefab);
            });
        }

        private void RegisterSceneLoader(DIContainer container)
        {
            container.RegisterAsSingle<ILoadingCurtain>(c =>
            {
                ResourcesAssetLoader resourcesAssetLoader = c.Resolve<ResourcesAssetLoader>();

                StandartLoadingCurtain standartLoadingCurtainPrefab = resourcesAssetLoader.LoadResource<StandartLoadingCurtain>(InfrastructureAssetPaths.StandartLoadingCurtainPath);

                return Instantiate(standartLoadingCurtainPrefab);
            });
        }

    }
}
