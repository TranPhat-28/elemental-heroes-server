﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using elemental_heroes_server.Data;

#nullable disable

namespace elemental_heroes_server.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SkillUser", b =>
                {
                    b.Property<int>("SkillsId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("SkillsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("SkillUser");
                });

            modelBuilder.Entity("UserWeapon", b =>
                {
                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.Property<int>("WeaponsId")
                        .HasColumnType("integer");

                    b.HasKey("UsersId", "WeaponsId");

                    b.HasIndex("WeaponsId");

                    b.ToTable("UserWeapon");
                });

            modelBuilder.Entity("elemental_heroes_server.Models.Hero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Attack")
                        .HasColumnType("integer");

                    b.Property<int>("AttackType")
                        .HasColumnType("integer");

                    b.Property<int>("BonusAttack")
                        .HasColumnType("integer");

                    b.Property<int>("BonusDefense")
                        .HasColumnType("integer");

                    b.Property<int>("BonusHp")
                        .HasColumnType("integer");

                    b.Property<int>("DamageType")
                        .HasColumnType("integer");

                    b.Property<int>("Defense")
                        .HasColumnType("integer");

                    b.Property<int>("Element")
                        .HasColumnType("integer");

                    b.Property<int>("Hp")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int?>("WeaponId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.HasIndex("WeaponId");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("elemental_heroes_server.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AttackType")
                        .HasColumnType("integer");

                    b.Property<int>("Damage")
                        .HasColumnType("integer");

                    b.Property<int>("DamageType")
                        .HasColumnType("integer");

                    b.Property<int>("Element")
                        .HasColumnType("integer");

                    b.Property<string>("IconUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("elemental_heroes_server.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Balance")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("elemental_heroes_server.Models.Weapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Element")
                        .HasColumnType("integer");

                    b.Property<string>("IconUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Weapons");
                });

            modelBuilder.Entity("SkillUser", b =>
                {
                    b.HasOne("elemental_heroes_server.Models.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("elemental_heroes_server.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserWeapon", b =>
                {
                    b.HasOne("elemental_heroes_server.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("elemental_heroes_server.Models.Weapon", null)
                        .WithMany()
                        .HasForeignKey("WeaponsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("elemental_heroes_server.Models.Hero", b =>
                {
                    b.HasOne("elemental_heroes_server.Models.User", "User")
                        .WithOne("Hero")
                        .HasForeignKey("elemental_heroes_server.Models.Hero", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("elemental_heroes_server.Models.Weapon", "Weapon")
                        .WithMany()
                        .HasForeignKey("WeaponId");

                    b.Navigation("User");

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("elemental_heroes_server.Models.User", b =>
                {
                    b.Navigation("Hero");
                });
#pragma warning restore 612, 618
        }
    }
}
