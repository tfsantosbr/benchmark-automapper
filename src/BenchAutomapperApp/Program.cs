using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BenchAutomapperApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<StandardVsAutomapper>();
        }
    }

    [MemoryDiagnoser]
    public class StandardVsAutomapper
    {
        private readonly List<CreateUser> _createUsers = new List<CreateUser>();
        private readonly List<User> _users = new List<User>();
        private IMapper _mapper;

        [GlobalSetup]
        public void GlobalSetup()
        {
            Enumerable
                .Range(1, 1000000)
                .ToList()
                .ForEach(i => _createUsers.Add(new CreateUser
                {
                    Name = "User",
                    Birthdate = new DateTime(1950, 5, 1),
                    Salary = 100000
                }));

            Enumerable
                .Range(1, 1000000)
                .ToList()
                .ForEach(i => _users.Add(new User("User", new DateTime(1950, 5, 1), 100000)));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Benchmark]
        public List<User> StandardCreate()
        {
            var users = new List<User>();

            foreach (var createUser in _createUsers)
                users.Add(new User(createUser.Name, createUser.Birthdate, createUser.Salary));

            return users;
        }

        [Benchmark]
        public List<User> AutomapperCreate()
        {
            var users = _mapper.Map<List<User>>(_createUsers);

            return users;
        }

        [Benchmark]
        public List<UserDetails> StandardGet()
        {
            var usersDetails = new List<UserDetails>();

            foreach (var user in _users)
            {
                usersDetails.Add(new UserDetails
                {
                    Id = user.Id,
                    Name = user.Name,
                    Birthdate = user.Birthdate,
                    Salary = user.Salary
                });
            }

            return usersDetails;
        }

        [Benchmark]
        public List<UserDetails> AutomapperGet()
        {
            return _mapper.Map<List<UserDetails>>(_users);
        }
    }

    public class User
    {
        public User(string name, DateTime birthdate, decimal salary)
        {
            Id = Guid.NewGuid();
            Name = name;
            Birthdate = birthdate;
            Salary = salary;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Birthdate { get; private set; }
        public decimal Salary { get; private set; }
    }

    public class UserDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public decimal Salary { get; set; }
    }

    public class CreateUser
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public decimal Salary { get; set; }
    }

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUser, User>()
               .ConstructUsing((createUser, context) =>
               {
                   return new User(createUser.Name, createUser.Birthdate, createUser.Salary);
               });

            CreateMap<User, UserDetails>();
        }
    }
}
