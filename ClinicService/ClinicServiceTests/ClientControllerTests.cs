using ClinicService.Controllers;
using Moq;
using System;
using System.Collection.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicServiceTests
{
    internal class ClientControllerTests
    {
        private ClientController _clientController;
        private Mock<ClientRepository> _mocClientRepository;
        public ClientControllerTests()
        {
            
            _mocClientRepository = new Mock<IClientRepository>();
            _clientController = new ClientController(_mocClientRepository.Object);
        }

        [Fact]
        public void GetAllClientsTest()
        {
            // [1.1] Подготовка данных для тестирования
                    
            // [1.2] 

            List<Client> list = new List<Client>();
            List.Add(new Client());
            List.Add(new Client());
            List.Add(new Client());

            _mocClientRepository.Setup(_mocClientRepository =>
                _mocClientRepository.GetAll()).Returns(list);

            // [2] Исполнение тестируемого метода
            
            var operationResultat = _clientController.GetAll();

            // [3] Подготовка эталонного результата, проверка результата 
            Assert.IsType<OkObjectResult>(operationResult.Result);
            Assert.IsAssignableFrom<List<Client>>(((OkObjectResult).operationResult).Value);

            _mocClientRepository.Verify(repository =>
            repository.GetAll, Times.AtLeastOnce());
        }

        public static readonly object[][] CorrectCreatClientData = 
        {
            new object[] { new DateTime(1985, 5, 20), "123 1234", "Иванов", "Андрей", "Сергеевич"}, 
            new object[] { new DateTime(1987, 2, 18), "123 2222", "Иванов", "Андрей", "Сергеевич"},
            new object[] { new DateTime(1979, 1, 22), "123 4321", "Иванов", "Андрей", "Сергеевич"},
        
        }

        [Theory]
        [MemberDate("CorrectCreatClientData")]
        public void CreateClientTest(DateTime birthday, string document, string surName, string firstName, string patronymic)
        {
            _mocClientRepository.Setup(repository =>
            repository
            .Create(It.IsNotNull<Client>()))
            .Returns(1).Verifiable();
            // .Create(It.IsAny<Client>())) - для второго теста
            // .Returns(1)
            // .Verifiable();



            var operationResult = _clientController.Create(new CreateClientRequest
            {
                Birthday = birthday,
                Document = document,
                SurName = surName,
                FirstName = firstName,
                Patronymic = patronymic
            });

            Assert.IsType<OkObjectResult>(operationResult.Result);
            Assert.IsAssignableFrom<int>(((OkObjectResult).operationResult).Value);
            _mocClientRepository.Verify(repository =>
            repository.Create(It.IsNotNull<Client>()), Times.AtLeastOnce());
            // repository.Create(It.IsAny<Client>()), Times.AtLeastOnce()); - для второго теста
        }
    }
}