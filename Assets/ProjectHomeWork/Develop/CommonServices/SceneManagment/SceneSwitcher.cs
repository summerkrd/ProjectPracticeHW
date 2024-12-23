﻿using Assets.ProjectHomeWork.Develop.CommonServices.CoroutinePerformer;
using Assets.ProjectHomeWork.Develop.CommonServices.LoadingScreen;
using Assets.ProjectHomeWork.Develop.DI;
using Assets.ProjectHomeWork.Develop.Gameplay.Infrastructure;
using Assets.ProjectHomeWork.Develop.MainMenu.Infrastructure;
using System;
using System.Collections;
using Object = UnityEngine.Object;

namespace Assets.ProjectHomeWork.Develop.CommonServices.SceneManagment
{
    public class SceneSwitcher
    {
        private const string ErrorSceneTransitionMessage = "Данный переход невозможен";

        private readonly DIContainer _projectContainer;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly ISceneLoader _sceneLoader;

        private DIContainer _currentSceneContainer;

        public SceneSwitcher(
            ICoroutinePerformer coroutinePerformer,
            ILoadingCurtain loadingCurtain,
            ISceneLoader sceneLoader,
            DIContainer projectContainer)
        {
            _coroutinePerformer = coroutinePerformer;
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
            _projectContainer = projectContainer;
        }

        public void ProcessSwitchSceneFor(IOutputSceneArgs outputSceneArgs)
        {
            switch (outputSceneArgs)
            {
                case OutputBootstrapArgs outputBootstrapArgs:
                    _coroutinePerformer.StartPerform(ProcessSwitchFromBootstrapScene(outputBootstrapArgs));
                    break;

                case OutputMainMenuArgs outputMainMenuArgs:
                    _coroutinePerformer.StartPerform(ProcessSwitchFromMainMenuScene(outputMainMenuArgs));
                    break;

                case OutputGameplayArgs outputGameplayArgs:
                    _coroutinePerformer.StartPerform(ProcessSwitchFromGameplayScene(outputGameplayArgs));
                    break;

                default:
                    throw new ArgumentException(nameof(outputSceneArgs));
            }
        }

        //___________________________________________________________________________________________

        private IEnumerator ProcessSwitchFromBootstrapScene(OutputBootstrapArgs outputBootstrapArgs)
        {
            switch (outputBootstrapArgs.NextSceneInputArgs)
            {
                case MainMenuInputArgs mainMenuInputArgs:
                yield return ProcessSwitchToMainMenuScene(mainMenuInputArgs);
                    break;

                default:
                    throw new ArgumentException(ErrorSceneTransitionMessage);
            }
        }

        private IEnumerator ProcessSwitchFromMainMenuScene(OutputMainMenuArgs outputMainMenuArgs)
        {
            switch (outputMainMenuArgs.NextSceneInputArgs)
            {
                case GameplayInputArgs gameplayInputArgs:
                yield return ProcessSwitchToGameplayScene(gameplayInputArgs);
                    break;

                default:
                    throw new ArgumentException(ErrorSceneTransitionMessage);
            }
        }

        private IEnumerator ProcessSwitchFromGameplayScene(OutputGameplayArgs outputGameplayArgs)
        {
            switch (outputGameplayArgs.NextSceneInputArgs)
            {
                case MainMenuInputArgs mainMenuInputArgs:
                yield return ProcessSwitchToMainMenuScene(mainMenuInputArgs);
                    break;

                default:
                    throw new ArgumentException(ErrorSceneTransitionMessage);
            }
        }

        //___________________________________________________________________________________________

        private IEnumerator ProcessSwitchToMainMenuScene(MainMenuInputArgs mainMenuInputArgs)
        {
            _loadingCurtain.Show();

            yield return _sceneLoader.LoadAsync(SceneID.Empty);
            yield return _sceneLoader.LoadAsync(SceneID.MainMenu);

            MainMenuBootstrap mainMenuBootstrap = Object.FindAnyObjectByType<MainMenuBootstrap>();

            if (mainMenuBootstrap == null)
                throw new NullReferenceException(nameof(mainMenuBootstrap));

            _currentSceneContainer = new DIContainer(_projectContainer);

            yield return mainMenuBootstrap.Run(_currentSceneContainer, mainMenuInputArgs);

            _loadingCurtain.Hide();
        }

        private IEnumerator ProcessSwitchToGameplayScene(GameplayInputArgs gameplayInputArgs)
        {
            _loadingCurtain.Show();

            yield return _sceneLoader.LoadAsync(SceneID.Empty);
            yield return _sceneLoader.LoadAsync(SceneID.Gameplay);

            GameplayBootstrap gameplayBootstrap = Object.FindAnyObjectByType<GameplayBootstrap>();

            if (gameplayBootstrap == null)
                throw new NullReferenceException(nameof(gameplayBootstrap));

            _currentSceneContainer = new DIContainer(_projectContainer);

            yield return gameplayBootstrap.Run(_currentSceneContainer, gameplayInputArgs);

            _loadingCurtain.Hide();
        }
    }
}