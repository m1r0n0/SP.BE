using SP.GraphQL.API.Queries;
using SP.GraphQL.DataAccessLayer.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();


var app = builder.Build();

app.MapGraphQL();

app.Run();