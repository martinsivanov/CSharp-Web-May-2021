using System;
using System.Collections.Generic;
using System.Text;

namespace SUS.MvcFramework
{
    public interface IServiceCollection
    {
        //  .Add <IUserService, UserService>
        void Add<TSource, IDestionation>();
        object CreateInstance(Type type);
    }
}
