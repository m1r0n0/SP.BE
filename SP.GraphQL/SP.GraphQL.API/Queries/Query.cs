﻿using SP.GraphQL.DataAccessLayer.Models;

namespace SP.GraphQL.API.Queries;

public class Query
{
    public Book GetBook() 
    {
       var book = new Book
        {
            Title = "C# in depth.",
            Author = new Author
            {
                Name = "Jon Skeet"
            }
        };
        return book;
    }
    
    public Service GetService() 
    {
        var service = new Service
        {
            Events = new List<Event>(),
            Name = "foo",
            Price = 100,
            ProviderUserId = "provider",
            ServiceId = 1
        };
        return service;
    }
    
}