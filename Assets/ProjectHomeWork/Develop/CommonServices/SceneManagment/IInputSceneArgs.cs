﻿namespace Assets.ProjectHomeWork.Develop.CommonServices.SceneManagment
{
    public interface IInputSceneArgs
    {

    }

    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(int levelNumber)
        {
            LevelNumber = levelNumber;
        }

        public int LevelNumber { get; }
    }

    public class MainMenuInputArgs : IInputSceneArgs
    {

    }
}
