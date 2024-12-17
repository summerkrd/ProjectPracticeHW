using Assets.ProjectHomeWork.Develop.DI;
using System.Collections;
using UnityEngine;

namespace Assets.ProjectHomeWork.Develop.EntryPoint
{
    public class Bootstrap : MonoBehaviour
    {
        //entryPoint - глобальная регистрация для старта проекта
        //bootstrap - инициализация начала работ

        public IEnumerator Run(DIContainer container)
        {
            //Включаем загрузочную штору после регистраций

            //Инициализация всех сервисов (подгрузка данных/конфигов/сервисов рекламы и тд)

            yield return new WaitForSeconds(1.5f); // типа какой то сервис инициализируется

            //скрываем штору

            //переход на следующую сцену с помощью сервиса смены сцен
        }
    }
}
