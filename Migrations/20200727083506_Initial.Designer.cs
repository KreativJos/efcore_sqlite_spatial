﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using test_sqlite;

namespace test_sqlite.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200727083506_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6");

            modelBuilder.Entity("test_sqlite.Marker", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Point>("Location")
                        .HasColumnType("POINT");

                    b.HasKey("Id");

                    b.ToTable("Markers");
                });
#pragma warning restore 612, 618
        }
    }
}
