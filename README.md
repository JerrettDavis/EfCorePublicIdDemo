# Entity Framework Core - Public ID Demo

Exposing internal, sequential primary IDs is widely considered a bad practice 
and a security concern. Most EF Core tutorials gloss-over the steps necessary to
implement a simple, reusable public identifier for your entities.

This simple project demonstrates creating a shared interface to denote all entities
that will potentially be fetched using externally-facing IDs. The approach shown
in this repo enables developers to simply place an `IPublicEntity` interface on 
all entities that should be configured.

## How it works

The `IPublicEntity` interface is placed on an entity, providing a `PublicId` property.
The `ApplicationDbContext`'s `OnModelCreating` method has been overridden to find
and place a value generator (`PublicIdGenerator`) on all entities that implement 
`IPublicEntity`. The `PublicIdGenerator` allows developers to swap out the 
implementation of `IUniqueIdGenerator` to suit their needs, but for demonstration 
purposes, the library `IdGen` has been used.

## What it does

The application attempts to create a SQLite database. If one already exists, it
attempts to update it if necessary. Once completed, it creates and saves a new
`MyEntity` object. The `MyEntity` object automatically creates a random `Name`
property when instantiated. When saved, the object is assigned a new `PublicId`
by Entity Framework and an `_id` by the underlying database provider.

After the `MyEntity` is created, the application fetches, enumerates, and logs
all the `MyEntities` in the database. Finally, the application selects a random 
entity from the fetched list, and uses its `PublicId` to fetch that item directly
from the database again.