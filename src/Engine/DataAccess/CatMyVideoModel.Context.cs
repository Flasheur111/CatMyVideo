﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Engine.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CatMyVideoEntities : DbContext
    {
        public CatMyVideoEntities()
            : base("name=CatMyVideoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<T_Comments> T_Comments { get; set; }
        public DbSet<T_Tags> T_Tags { get; set; }
        public DbSet<T_Users> T_Users { get; set; }
        public DbSet<T_Videos> T_Videos { get; set; }
    }
}