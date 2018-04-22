using Autofac;
using Autofac.Integration.WebApi;
using FluentValidation;
using SimpleWebApi.Core;
using SimpleWebApi.Core.Models.Authorization;
using SimpleWebApi.Core.Models.Notes;
using SimpleWebApi.Core.Repositories.Notes;
using SimpleWebApi.Core.Repositories.Users;
using SimpleWebApi.Core.Services.Authorization;
using SimpleWebApi.Core.Services.Notes;
using SimpleWebApi.Core.Utilities;
using SimpleWebApi.Repositories.MongoDB;
using SimpleWebApi.Repositories.MongoDB.Notes;
using SimpleWebApi.Repositories.MongoDB.Users;
using System.Reflection;
using System.Web.Http;

namespace SimpleWebApi.Web
{
    public class Bootstrapper
    {
        private static IContainer _container;

        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Services
            builder.RegisterType<AuthorizationService>().As<IAuthorizationService>();
            builder.RegisterType<NotesService>().As<INotesService>();

            //Repositories
            builder.RegisterType<MongoDBConfiguration>();
            builder.RegisterType<UsersRepository>().As<IUsersRepository>();
            builder.RegisterType<NotesRepository>().As<INotesRepository>();

            //Validators
            builder.RegisterType<RegisterModelValidator>().As<IValidator<RegisterRequest>>();
            builder.RegisterType<LoginRequestValidator>().As<IValidator<LoginRequest>>();
            builder.RegisterType<NoteModelValidator>().As<IValidator<NoteRequest>>();

            //Utilities
            builder.RegisterType<TokenProvider>().As<ITokenProvider>();

            //Configuration
            builder.RegisterInstance(new AppConfiguration()
            {                                               
                DbConnectionString = "CONNECTION_STRING" ,
                JwtSecretKey = "M3kp83O4j0tB20asS6aa2j18L7g1OnBc"
            });

            // Set the dependency resolver to be Autofac.
            _container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(_container);
        }

        public static T Resolve<T>()
        {
            if (_container == null) return default(T);

            return _container.Resolve<T>();
        }
    }
}