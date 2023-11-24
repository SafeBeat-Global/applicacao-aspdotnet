using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalSolution.Migrations
{
    public partial class RelacionamentoMuitosParaMuitos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonitorPaciente_Monitor_MonitorsId",
                table: "MonitorPaciente");

            migrationBuilder.DropForeignKey(
                name: "FK_MonitorPaciente_Paciente_PacientesId",
                table: "MonitorPaciente");

            migrationBuilder.RenameColumn(
                name: "PacientesId",
                table: "MonitorPaciente",
                newName: "PacienteId");

            migrationBuilder.RenameColumn(
                name: "MonitorsId",
                table: "MonitorPaciente",
                newName: "MonitorId");

            migrationBuilder.RenameIndex(
                name: "IX_MonitorPaciente_PacientesId",
                table: "MonitorPaciente",
                newName: "IX_MonitorPaciente_PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonitorPaciente_Monitor_MonitorId",
                table: "MonitorPaciente",
                column: "MonitorId",
                principalTable: "Monitor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MonitorPaciente_Paciente_PacienteId",
                table: "MonitorPaciente",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonitorPaciente_Monitor_MonitorId",
                table: "MonitorPaciente");

            migrationBuilder.DropForeignKey(
                name: "FK_MonitorPaciente_Paciente_PacienteId",
                table: "MonitorPaciente");

            migrationBuilder.RenameColumn(
                name: "PacienteId",
                table: "MonitorPaciente",
                newName: "PacientesId");

            migrationBuilder.RenameColumn(
                name: "MonitorId",
                table: "MonitorPaciente",
                newName: "MonitorsId");

            migrationBuilder.RenameIndex(
                name: "IX_MonitorPaciente_PacienteId",
                table: "MonitorPaciente",
                newName: "IX_MonitorPaciente_PacientesId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonitorPaciente_Monitor_MonitorsId",
                table: "MonitorPaciente",
                column: "MonitorsId",
                principalTable: "Monitor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MonitorPaciente_Paciente_PacientesId",
                table: "MonitorPaciente",
                column: "PacientesId",
                principalTable: "Paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
