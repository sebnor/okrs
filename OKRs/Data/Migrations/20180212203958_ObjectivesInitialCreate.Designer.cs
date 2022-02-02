﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using OKRs.Web.Data;
using System;

namespace OKRs.Migrations
{
    [DbContext(typeof(ObjectivesDbContext))]
    [Migration("20180212203958_ObjectivesInitialCreate")]
    partial class ObjectivesInitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OKRs.Models.KeyResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<Guid?>("ObjectiveId");

                    b.HasKey("Id");

                    b.HasIndex("ObjectiveId");

                    b.ToTable("KeyResults");
                });

            modelBuilder.Entity("OKRs.Models.Objective", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Title");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Objectives");
                });

            modelBuilder.Entity("OKRs.Models.KeyResult", b =>
                {
                    b.HasOne("OKRs.Models.Objective")
                        .WithMany("KeyResults")
                        .HasForeignKey("ObjectiveId");
                });
#pragma warning restore 612, 618
        }
    }
}
