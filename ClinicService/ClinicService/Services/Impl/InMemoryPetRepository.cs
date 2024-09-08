using ClinicService.Models;
using System.Collections.Generic;

namespace ClinicService.Services.impl
{
    public class InMemoryPetRepository : IPetRepository
    {
        private readonly List<Pet> _pets = new List<Pet>();

        public int Create(Pet item)
        {
            _pets.Add(item);
            return 1; // Предполагаем успешное выполнение
        }

        public int Update(Pet item)
        {
            var pet = _pets.FirstOrDefault(p => p.PetId == item.PetId);
            if (pet == null)
            {
                return 0; // Если не найдено, возвращаем 0
            }
            pet.Name = item.Name ?? pet.Name;
            pet.Birthday = item.Birthday;
            pet.ClientId = item.ClientId;
            return 1;
        }

        public int Delete(int id)
        {
            var pet = _pets.FirstOrDefault(p => p.PetId == id);
            if (pet != null)
            {
                _pets.Remove(pet);
                return 1;
            }
            return 0;
        }

        public Pet GetById(int id)
        {
            return _pets.FirstOrDefault(p => p.PetId == id) ?? new Pet();
        }

        public List<Pet> GetAll()
        {
            return _pets;
        }
    }
}
