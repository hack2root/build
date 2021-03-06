﻿using Build;

namespace TestSet0
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
        public ServiceDataRepository([Injection(typeof(SqlDataRepository), 2018)]IPersonRepository repository)
        {
            Repository = repository;
        }

        public IPersonRepository Repository { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class SqlDataRepository : IPersonRepository
    {
        public SqlDataRepository()
        {
        }

        public SqlDataRepository(int personId)
        {
            PersonId = personId;
        }

        public int PersonId { get; }

        public Person GetPerson(int personId) => new(this);
    }

    class DefaultSqlDataRepository : IPersonRepository
    {
        [Dependency]
        public DefaultSqlDataRepository([Injection] int personId) => PersonId = personId;

        public int PersonId { get; }

        public Person GetPerson(int personId) => new(this);
    }

    class PrivateSqlDataRepository : IPersonRepository
    {
        public PrivateSqlDataRepository()
        {
        }

        public PrivateSqlDataRepository(int personId)
        {
            PersonId = personId;
        }

        public int PersonId { get; }

        public Person GetPerson(int personId) => new(this);
    }
}