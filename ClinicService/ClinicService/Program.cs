
using ClinicService.Services;
using ClinicService.Services.impl;
using Microsoft.Data.Sqlite;

namespace ClinicService
{
    public class Program
    {
        //https://sqlitestudio.pl/
        public static void Main(string[] args)
        {
            //ConfigureSqlLiteConnection();
            

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IPetRepository, PetRepository>();

            

            builder.Services.AddControllers();
            

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void ConfigureSqlLiteConnection()
        {
            const string connectionString = "Data Source = clinic.db;";
            SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
        }

        private static void PrepareSchema(SqliteConnection connection)
        {
            // Задаем новый текст команды  для выполнения
            // Удаляем таблицу с метриками, если она есть в БД
            SqliteCommand sqliteCommand = connection.CreateCommand();
            sqliteCommand.CommandText = "DROP TABLE IF EXISTS consultations";
            sqliteCommand.ExecuteNonQuery();
            sqliteCommand.CommandText = "DROP TABLE IF EXISTS pets";
            sqliteCommand.ExecuteNonQuery();
            sqliteCommand.CommandText = "DROP TABLE IF EXISTS clients";
            sqliteCommand.ExecuteNonQuery();
            
            sqliteCommand.CommandText =
                    @"CREATE TABLE Clients(ClientId INTEGER PRIMARY KEY,
                    Document TEXT,
                    SurName TEXT,
                    FirstName TEXT,
                    Patronymic TEXT,
                    Birthday INTEGER)";
            sqliteCommand.ExecuteNonQuery(); 
            sqliteCommand.CommandText =
                    @"CREATE TABLE Pets(PetId INTEGER PRIMARY KEY,
                    Clientid INTEGER,
                    Name TEXT,
                    Birthday INTEGER)";
            sqliteCommand.ExecuteNonQuery();
            sqliteCommand.CommandText =
                    @"CREATE TABLE Consultations(ConsultationId INTEGER PRIMARY KEY,
                    Clientid INTEGER,
                    PetId INTEGER,
                    ConsultationDate INTEGER
                    Description TEXT)";               
            sqliteCommand.ExecuteNonQuery();          
        }
    }
}


//http://localhost:5111/swagger/v1/swagger.json