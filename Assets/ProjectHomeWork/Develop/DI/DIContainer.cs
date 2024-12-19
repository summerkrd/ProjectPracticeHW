using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.TextCore.Text;

namespace Assets.ProjectHomeWork.Develop.DI
{
    public class DIContainer
    {
        private readonly Dictionary<Type, Registration> _container = new();
        private readonly DIContainer _parent;
        private readonly List<Type> _requests = new();

        public DIContainer() : this(null) { }

        public DIContainer(DIContainer parent) => _parent = parent;

        public void RegisterAsSingle<T>(Func<DIContainer, T> сreator) //Метод регистрации зависимости
        {
            if (_container.ContainsKey(typeof(T)))
                throw new InvalidOperationException(typeof(T) + " Alredy register"); //Проверяем, не содержится ли уже регистрируемый тип в словаре

            //Registration registration = new Registration((DIContainer container) => сreator.Invoke(container));
            //Registration registration = new Registration(container => сreator(container)); // Создаем регистрацию      
            Registration registration = new Registration(сreator); // Создаем регистрацию      

            _container.Add(typeof(T), registration); //Добавляем способ регистрации в словарь
            //_container[typeof(T)] = registration;
        }       

        public T Resolve<T>() //Метод получения зависимости
        {
            if (_requests.Contains(typeof(T)))
                throw new InvalidOperationException($"Cycle resolve for {typeof(T)}");

            _requests.Add(typeof(T));

            try
            {
                if (_container.TryGetValue(typeof(T), out Registration registration))
                    return CreateFrom<T>(registration);

                if (_parent != null)
                    return _parent.Resolve<T>();
            }
            finally
            {
                _requests.Remove(typeof(T));
            }

            throw new InvalidOperationException($"Registration for {typeof(T)} not exist");
        }

        private T CreateFrom<T>(Registration registration)
        {
            if (registration.Instance == null && registration.Creator != null)
                registration.Instance = registration.Creator/*.Invoke*/(this);

            return (T)registration.Instance;
        }

        public class Registration
        {
            public Func<DIContainer, object> Creator { get; }
            public object Instance { get; set; } //Ссылка на созданный сервис

            public Registration(object instance) => Instance = instance;

            public Registration(Func<DIContainer, object> creator) => Creator = creator;
        }
    }
}
