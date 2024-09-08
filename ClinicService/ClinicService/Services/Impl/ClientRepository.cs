using ClinicService.Services;
using ClinicService.Models;
using Microsoft.Data.Sqlite;


namespace ClinicService.Services.impl
{
    public class ClientRepository : IClientRepository
    {
        private const string connectionString = "Data Source = clinic.db";
        public int Create(Client item)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                // Прописывает в команду SQL-запрос на добавление данных
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO  clients(Document, SurName, FirstName, Patronymic, Birthday) VALUES(@Document,@SurName, @FirstName, @Patronymic, @Birthday)";
                command.Parameters.AddWithValue("@Document", item.Document ?? string.Empty);
                command.Parameters.AddWithValue("@SurName", item.SurName ?? string.Empty);
                command.Parameters.AddWithValue("@FirstName", item.FirstName ?? string.Empty);
                command.Parameters.AddWithValue("@Patronymic", item.Patronymic ?? string.Empty);
                command.Parameters.AddWithValue("@Birthday", item.Birthday.Ticks);
                // Подготовка команды к выполнению
                command.Prepare();
                // Выполнение команды
                return command.ExecuteNonQuery();
            }

        }

        public int Update(Client item)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
            connection.Open();
            // Прописывает в команду SQL-запрос на добавление данных
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE clients SET Document = @Document, SurName = @SurName, FirstName = @FirstName, Patronymic = @Patronymic, Birthday = @Birthday WHERE ClientId = @ClientId";
            
            command.Parameters.AddWithValue("@ClientId", item.ClientId);
            command.Parameters.AddWithValue("@Document", item.Document ?? string.Empty);
            command.Parameters.AddWithValue("@SurName", item.SurName ?? string.Empty);
            command.Parameters.AddWithValue("@FirstName", item.FirstName ?? string.Empty);
            command.Parameters.AddWithValue("@Patronymic", item.Patronymic ?? string.Empty);
            command.Parameters.AddWithValue("@Birthday", item.Birthday.Ticks);
            // Подготовка команды к выполнению
            command.Prepare();
            // Выполнение команды
            return command.ExecuteNonQuery();
            }
        }

        public int Delete(int id)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                // Прописывает в команду SQL-запрос на добавление данных
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM clients  WHERE ClientId = @ClientId";
                command.Parameters.AddWithValue("@ClientId", id);
                // Подготовка команды к выполнению
                command.Prepare();
                // Выполнение команды
                return command.ExecuteNonQuery();
            } 
        }

        public Client GetById(int id)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
            connection.Open();
            // Прописывает в команду SQL-запрос 
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM clients WHERE ClientId = @ClientId";
            command.Parameters.AddWithValue("@ClientId", id);
            // Подготовка команды к выполнению
            command.Prepare();            
            // Выполнение команды
            SqliteDataReader reader = command.ExecuteReader();
            
            if (reader.Read())
            {
                return new Client
                {
                    ClientId = reader.GetInt32(0),
                    Document = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    SurName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    FirstName = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                    Patronymic = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                    Birthday = new DateTime(reader.GetInt64(5))
                    };
                
                }
            
            }
            return null!; 
        }

        public List<Client> GetAll()
        {
            List<Client> list = new List<Client>();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                // Прописывает в команду SQL-запрос 
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = " SELECT * FROM clients ";
                // Подготовка команды к выполнению
                command.Prepare();
                // Выполнение команды
                SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Client client = new Client
                {
                    ClientId = reader.GetInt32(0),
                    Document = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    SurName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    FirstName = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                    Patronymic = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                    Birthday = new DateTime(reader.GetInt64(5))
                };
                list.Add(client);
            }
            
            }
            return list; 
        }

    }
}