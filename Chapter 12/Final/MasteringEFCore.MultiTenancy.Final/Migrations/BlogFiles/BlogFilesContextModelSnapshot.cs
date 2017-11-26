using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MasteringEFCore.MultiTenancy.Final.Data;

namespace MasteringEFCore.MultiTenancy.Final.Migrations.BlogFiles
{
    [DbContext(typeof(BlogFilesContext))]
    partial class BlogFilesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MasteringEFCore.MultiTenancy.Final.Models.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Content");

                    b.Property<string>("ContentDisposition");

                    b.Property<string>("ContentType");

                    b.Property<string>("FileName");

                    b.Property<long>("Length");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });
        }
    }
}
