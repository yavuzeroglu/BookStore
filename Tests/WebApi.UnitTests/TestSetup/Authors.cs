using System;
using Webapi.DBOperations;
using Webapi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
            new Author{ Name = "Frank Herbert", Birthday = new DateTime(1920,10,08)},
            new Author{ Name = "Eric Ries", Birthday = new DateTime(1978,09,22)},
            new Author{ Name= "Charlotte Perskins", Birthday = new DateTime(1890, 07, 03)}
            );
        }

    }
}