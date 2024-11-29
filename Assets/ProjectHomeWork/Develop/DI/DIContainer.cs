using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ProjectHomeWork.Develop.DI
{
    public class DIContainer
    {
        public class Registration
        {
            public Func<DIContainer, object> Creator { get; }
            public object Instance { get; set; }

            public Registration(object instance) => Instance = instance;

            public Registration(Func<DIContainer, object> creator) => Creator = creator;
        }
    }
}
