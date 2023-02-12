using QuizletClone.Domain.Model;
using QuizletClone.Domain.Services;
using QuizletClone.EntityFramework.Services;

IDataService<User> userService = new GenericDataService<User>(new QuizletClone.EntityFramework.QuizletCloneDbContextFactory());
Console.WriteLine(userService.GetAll().Result.Count());