﻿using Build;

namespace TestSet8
{
    public interface IPersonRepository
    {
        Person GetPerson(int personId);
    }

    public class Person
    {
        readonly IPersonRepository _personRepository;

        public Person(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IPersonRepository Repository { get { return _personRepository; } }
    }

    public class ServiceDataRepository : IPersonRepository
    {
        public ServiceDataRepository([Injection(typeof(SqlDataRepository))]IPersonRepository repository)
        {
            Repository = repository;
        }

        public IPersonRepository Repository { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class SqlDataRepository : IPersonRepository
    {
        private readonly int _personId;

        public SqlDataRepository()
        {
        }

        [Dependency(typeof(SqlDataRepository))]
        public SqlDataRepository(int personId)
        {
            _personId = personId;
        }

        public int Id { get {return _personId; } }

        public Person GetPerson(int personId) => new(this);
    }
}