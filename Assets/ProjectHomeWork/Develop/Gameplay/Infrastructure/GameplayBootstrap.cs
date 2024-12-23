using Assets.ProjectHomeWork.Develop.CommonServices.SceneManagment;
using Assets.ProjectHomeWork.Develop.DI;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.ProjectHomeWork.Develop.Gameplay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private DIContainer _container;

        public IEnumerator Run(DIContainer container, GameplayInputArgs gameplayInputArgs)
        {
            _container = container;

            PrcessRegistrations();

            Debug.Log($"Подгружаем ресурсы для уровня {gameplayInputArgs.LevelNumber}");
            Debug.Log("Создаем персонажа");
            Debug.Log("Сцена готова, можно начинать игру");

            yield return new WaitForSeconds(1f);
        }

        private void PrcessRegistrations()
        {
            // Регистрации для сцены геймплея
        }
    }
}
