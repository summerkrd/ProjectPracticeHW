using Assets.ProjectHomeWork.Develop.DI;
using UnityEngine;

namespace Assets.ProjectHomeWork.Develop.EntryPoint
{
    public class EntryPoint :MonoBehaviour
    {
        [SerializeField] private Bootstrap _gameBootstrap;
        private void Awake()
        {
            SetupAppSettings();

            DIContainer projectContainer = new DIContainer();

            //регистрация сервисов на целый проект
            //аналог global context из популярных DI фреймворках
            //родительский контейнер для всех будущих

            //_gameBootstrap через сервис корутины запустим Run() и передадим 
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
    }
}
