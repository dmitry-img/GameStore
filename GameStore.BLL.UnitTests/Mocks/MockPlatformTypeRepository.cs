using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Moq;

namespace GameStore.BLL.UnitTests.Mocks
{
    class MockPlatformTypeRepository
    {
        public static Mock<IGenericRepository<PlatformType>> GetRepository()
        {
            var platformTypes = new List<PlatformType>()
            {
                new PlatformType() { Id = 1, Type = "Mobile" },
                new PlatformType() { Id = 2, Type = "Browser" },
                new PlatformType() { Id = 3, Type = "Desktop" },
                new PlatformType() { Id = 4, Type = "Console" },
            };

            var mockRepo = new Mock<IGenericRepository<PlatformType>>();

            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(platformTypes);

            mockRepo.Setup(r => r.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => platformTypes.FirstOrDefault(g => g.Id == id));

            mockRepo.Setup(r => r.Create(It.IsAny<PlatformType>())).Callback((PlatformType leaveType) =>
            {
                platformTypes.Add(leaveType);
            });

            mockRepo.Setup(r => r.FilterAsync(It.IsAny<Expression<Func<PlatformType, bool>>>()))
               .ReturnsAsync((Expression<Func<PlatformType, bool>> expression) =>
               {
                   return platformTypes.Where(expression.Compile()).ToList();
               });

            return mockRepo;
        }
    }
}
