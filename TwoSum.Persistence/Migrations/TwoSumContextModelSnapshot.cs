﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TwoSum.Domain.Solution;
using TwoSum.Persistence.Context;

#nullable disable

namespace TwoSum.Persistence.Migrations
{
    [DbContext(typeof(TwoSumContext))]
    partial class TwoSumContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Enterprise.Persistence.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Error")
                        .HasColumnType("text");

                    b.Property<DateTime>("OccuredOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages", (string)null);
                });

            modelBuilder.Entity("TwoSum.Domain.Solution.Solution", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int[]>("Nums")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<SolutionStatus.SolutionStatusEnum>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("Target")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Solutions", (string)null);
                });

            modelBuilder.Entity("TwoSum.Domain.Solution.SolutionIteration", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Index")
                        .HasColumnType("integer");

                    b.Property<string>("Result")
                        .HasColumnType("text");

                    b.Property<Guid>("SolutionId")
                        .HasColumnType("uuid");

                    b.Property<SolutionIterationStatus.SolutionIterationStatusEnum>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SolutionId", "Index")
                        .IsUnique();

                    b.ToTable("SolutionIterations", (string)null);
                });

            modelBuilder.Entity("TwoSum.Domain.Solution.SolutionIteration", b =>
                {
                    b.HasOne("TwoSum.Domain.Solution.Solution", "Solution")
                        .WithMany("Iterations")
                        .HasForeignKey("SolutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Solution");
                });

            modelBuilder.Entity("TwoSum.Domain.Solution.Solution", b =>
                {
                    b.Navigation("Iterations");
                });
#pragma warning restore 612, 618
        }
    }
}
