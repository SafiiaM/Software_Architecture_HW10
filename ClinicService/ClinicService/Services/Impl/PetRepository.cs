using ClinicService.Models;
using ClinicService.Services;
using Microsoft.Data.Sqlite;

namespace ClinicService.Services.impl
{
    public class PetRepository : IPetRepository
    {
        private const string connectionString = "Data Source = clinic.db";

        public int Create(Pet item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Pet object cannot be null.");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO pets (ClientId, Name, Birthday) VALUES (@ClientId, @Name, @Birthday)";
                command.Parameters.AddWithValue("@ClientId", item.ClientId);
                command.Parameters.AddWithValue("@Name", item.Name ?? string.Empty);
                command.Parameters.AddWithValue("@Birthday", item.Birthday.Ticks);
                command.Prepare();
                return command.ExecuteNonQuery();
            }
        }

        public int Update(Pet item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Pet object cannot be null.");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE pets SET ClientId = @ClientId, Name = @Name, Birthday = @Birthday WHERE PetId = @PetId";
                command.Parameters.AddWithValue("@PetId", item.PetId);
                command.Parameters.AddWithValue("@ClientId", item.ClientId);
                command.Parameters.AddWithValue("@Name", item.Name ?? string.Empty);
                command.Parameters.AddWithValue("@Birthday", item.Birthday.Ticks);
                command.Prepare();
                return command.ExecuteNonQuery();
            }
        }

        public int Delete(int id)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM pets WHERE PetId = @PetId";
                command.Parameters.AddWithValue("@PetId", id);
                command.Prepare();
                return command.ExecuteNonQuery();
            }
        }

        public Pet GetById(int id)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM pets WHERE PetId = @PetId";
                command.Parameters.AddWithValue("@PetId", id);
                command.Prepare();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Pet pet = new Pet
                        {
                            PetId = reader.GetInt32(0),
                            ClientId = reader.GetInt32(1),
                            Name = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                            Birthday = new DateTime(reader.GetInt64(3))
                        };
                        return pet;
                    }
                }
            }
            return null!;
        }

        public List<Pet> GetAll()
        {
            List<Pet> list = new List<Pet>();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM pets";
                command.Prepare();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pet pet = new Pet
                        {
                            PetId = reader.GetInt32(0),
                            ClientId = reader.GetInt32(1),
                            Name = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                            Birthday = new DateTime(reader.GetInt64(3))
                        };
                        list.Add(pet);
                    }
                }
            }
            return list;
        }
    }
}
