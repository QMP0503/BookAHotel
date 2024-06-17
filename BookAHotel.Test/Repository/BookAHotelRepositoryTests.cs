using BookAHotel.Data;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using BookAHotel.Models;
using System.Runtime.CompilerServices;
using BookAHotel.Repository;
using FluentAssertions;

namespace BookAHotel.Test.Repository
{
    public class BookAHotelRepositoryTests
    {
        private async Task<HotelBookingContext> GetDatabaseContext()
        {
            var option = new DbContextOptionsBuilder<HotelBookingContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new HotelBookingContext(option);
            databaseContext.Database.EnsureCreated();
            if(await databaseContext.Clients.CountAsync() == 0)
            {
                for(int i = 0; i < 5; i++)
                {
                    await databaseContext.Clients.AddAsync(new Client
                    {
                        Name = "Client" + i
                    });
                }
                await databaseContext.SaveChangesAsync();
            }
            if(await databaseContext.RoomTypes.CountAsync() == 0)
            {
                await databaseContext.RoomTypes.AddRangeAsync(
                    new RoomType
                    {
                        RoomTypeName = RoomTypeNameEnum.Normal,
                        Price = 50
                    },
                    new RoomType
                    {
                        RoomTypeName = RoomTypeNameEnum.Queen,
                        Price = 80
                    },
                    new RoomType
                    {
                        RoomTypeName = RoomTypeNameEnum.King,
                        Price = 120
                    },
                    new RoomType
                    {
                        RoomTypeName = RoomTypeNameEnum.President,
                        Price = 200
                    }); 
            }
            if(await databaseContext.Rooms.CountAsync() == 0)
            {
                for(int i = 0; i < 4; i++)
                {
                    await databaseContext.Rooms.AddAsync(new Room
                    {
                        Name = "Room " + i+1,
                        RoomTypeId = i + 1,
                        IsAvailable = true,
                        RoomType = databaseContext.RoomTypes.FirstOrDefault(x => x.Id == i + 1)
                    });
                }
                
            }
            await databaseContext.SaveChangesAsync();
            return databaseContext;
        }
        [Fact]        
        public async Task BookAHotelClientRepository_FindBy_ReturnsClient()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var predicate = new Func<Client, bool>(x => x.Id == 1);
            var clientRepository = new ClientRepository(dbContext);

            //Act
            var result = clientRepository.FindBy(predicate);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Client>();
            result.Should().BeEquivalentTo(dbContext.Clients.FirstOrDefault(x => x.Id == 1));
        }
        [Fact]
        public async Task BookAHotelClientRepository_ListBy_ReturnsClientList()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var predicate = new Func<Client, bool>(x => x.Id > 1);
            var clientRepository = new ClientRepository(dbContext);
            
            //Act
            var result = clientRepository.ListBy(predicate);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Client>>();
            result.Should().BeEquivalentTo(dbContext.Clients.Where(x => x.Id > 1).ToList());
        }
    }
}
