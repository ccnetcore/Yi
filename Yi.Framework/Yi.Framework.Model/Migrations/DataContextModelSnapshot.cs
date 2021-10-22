﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Yi.Framework.Model;

namespace Yi.Framework.Model.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("Yi.Framework.Model.Models.menu", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("icon")
                        .HasColumnType("longtext");

                    b.Property<int>("is_delete")
                        .HasColumnType("int");

                    b.Property<int>("is_show")
                        .HasColumnType("int");

                    b.Property<int>("is_top")
                        .HasColumnType("int");

                    b.Property<string>("menu_name")
                        .HasColumnType("longtext");

                    b.Property<int?>("menuid")
                        .HasColumnType("int");

                    b.Property<int?>("mouldid")
                        .HasColumnType("int");

                    b.Property<string>("router")
                        .HasColumnType("longtext");

                    b.Property<int>("sort")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("menuid");

                    b.HasIndex("mouldid");

                    b.ToTable("menu");
                });

            modelBuilder.Entity("Yi.Framework.Model.Models.mould", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("is_delete")
                        .HasColumnType("int");

                    b.Property<string>("mould_name")
                        .HasColumnType("longtext");

                    b.Property<string>("url")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("mould");
                });

            modelBuilder.Entity("Yi.Framework.Model.Models.role", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("introduce")
                        .HasColumnType("longtext");

                    b.Property<int>("is_delete")
                        .HasColumnType("int");

                    b.Property<string>("role_name")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("role");
                });

            modelBuilder.Entity("Yi.Framework.Model.Models.user", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("address")
                        .HasColumnType("longtext");

                    b.Property<int?>("age")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .HasColumnType("longtext");

                    b.Property<string>("icon")
                        .HasColumnType("longtext");

                    b.Property<string>("introduction")
                        .HasColumnType("longtext");

                    b.Property<string>("ip")
                        .HasColumnType("longtext");

                    b.Property<int>("is_delete")
                        .HasColumnType("int");

                    b.Property<string>("nick")
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .HasColumnType("longtext");

                    b.Property<int?>("phone")
                        .HasColumnType("int");

                    b.Property<string>("username")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("menurole", b =>
                {
                    b.Property<int>("menusid")
                        .HasColumnType("int");

                    b.Property<int>("rolesid")
                        .HasColumnType("int");

                    b.HasKey("menusid", "rolesid");

                    b.HasIndex("rolesid");

                    b.ToTable("menurole");
                });

            modelBuilder.Entity("roleuser", b =>
                {
                    b.Property<int>("rolesid")
                        .HasColumnType("int");

                    b.Property<int>("usersid")
                        .HasColumnType("int");

                    b.HasKey("rolesid", "usersid");

                    b.HasIndex("usersid");

                    b.ToTable("roleuser");
                });

            modelBuilder.Entity("Yi.Framework.Model.Models.menu", b =>
                {
                    b.HasOne("Yi.Framework.Model.Models.menu", null)
                        .WithMany("children")
                        .HasForeignKey("menuid");

                    b.HasOne("Yi.Framework.Model.Models.mould", "mould")
                        .WithMany()
                        .HasForeignKey("mouldid");

                    b.Navigation("mould");
                });

            modelBuilder.Entity("menurole", b =>
                {
                    b.HasOne("Yi.Framework.Model.Models.menu", null)
                        .WithMany()
                        .HasForeignKey("menusid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yi.Framework.Model.Models.role", null)
                        .WithMany()
                        .HasForeignKey("rolesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("roleuser", b =>
                {
                    b.HasOne("Yi.Framework.Model.Models.role", null)
                        .WithMany()
                        .HasForeignKey("rolesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yi.Framework.Model.Models.user", null)
                        .WithMany()
                        .HasForeignKey("usersid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Yi.Framework.Model.Models.menu", b =>
                {
                    b.Navigation("children");
                });
#pragma warning restore 612, 618
        }
    }
}
