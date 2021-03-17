﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoPlus.Migrations
{
    public partial class TempoTotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalTempo",
                table: "RegistroPontos",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalTempo",
                table: "RegistroPontos");
        }
    }
}
