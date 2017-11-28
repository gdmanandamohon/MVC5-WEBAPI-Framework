using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace WebClient.Configuration
{
    public class ServiceLocator
    {
        private static IUnityContainer _container;

        public static void RegisterMappings(IUnityContainer container)
        {
            _container = container;

            //container.RegisterType<IUserRepository, UserDataAccess>();
            //container.RegisterType<IGenericRepository<UserModel>, GenericDataAccess<UserModel>>();


        }
        public static T GetInstance<T>()
        {
            try
            {
                return _container.Resolve<T>();
            }
            catch
            {
                return default(T);
            }
        }
    }
}