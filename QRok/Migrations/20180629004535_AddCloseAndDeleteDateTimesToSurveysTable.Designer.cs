﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QRok.Models;

namespace QRok.Migrations
{
    [DbContext(typeof(QRokContext))]
    [Migration("20180629004535_AddCloseAndDeleteDateTimesToSurveysTable")]
    partial class AddCloseAndDeleteDateTimesToSurveysTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QRok.Models.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CloseDateTime");

                    b.Property<DateTime>("DeleteDateTime");

                    b.Property<Guid>("Guid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("QRok.Models.SurveyOption", b =>
                {
                    b.Property<int>("SurveyId");

                    b.Property<int>("OptionNumber");

                    b.Property<int>("Count");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("SurveyId", "OptionNumber");

                    b.ToTable("SurveyOptions");
                });

            modelBuilder.Entity("QRok.Models.SurveyOption", b =>
                {
                    b.HasOne("QRok.Models.Survey", "Survey")
                        .WithMany("SurveyOptions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}