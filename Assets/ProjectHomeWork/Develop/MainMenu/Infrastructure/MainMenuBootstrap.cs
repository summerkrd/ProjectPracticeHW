using Assets.ProjectHomeWork.Develop.CommonServices.SceneManagment;
using Assets.ProjectHomeWork.Develop.DI;
using System.Collections;
using UnityEngine;

namespace Assets.ProjectHomeWork.Develop.MainMenu.Infrastructure
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        private DIContainer _container;

        public IEnumerator Run(DIContainer container, MainMenuInputArgs mainMenuInputArgs)
        {
            _container = container;

            PrcessRegistrations();

            yield return new WaitForSeconds(1f);
        }

        private void PrcessRegistrations()
        {
            // Регистрации для сцены меню
        }
    }
}
