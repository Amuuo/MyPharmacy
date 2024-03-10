using System.Data;
using Dapper;
using Moq;
using MyPharmacy.Core.Utilities;
using MyPharmacy.Core.Utilities.Interfaces;
using MyPharmacy.Data.Entities;
using NUnit.Framework;

namespace MyPharmacy.Data.Repository;

[TestFixture]
public class PharmacyRepositoryTests
{
    private PharmacyRepository  _pharmacyRepository;
    private Mock<IDbConnection> _dbConnectionMock;

    [SetUp]
    public void Setup()
    {
        _dbConnectionMock   = new Mock<IDbConnection>();
        _pharmacyRepository = new PharmacyRepository(_dbConnectionMock.Object);
    }

    [Test]
    public async Task GetPharmacyListPagedAsync_ShouldReturnPagedResult()
    {
        // Arrange
        var pagingInfo = new PagingInfo { Page = 1, Take = 10 };
        var pharmacies = new[] { new Pharmacy { Id = 1, Name = "Pharmacy 1" }, new Pharmacy { Id = 2, Name = "Pharmacy 2" } };
        var parameters = new DynamicParameters();
        parameters.Add("Total", 2);
        parameters.Add("Pages", 1);

        //_dbConnectionMock.Setup(c => c.QueryAsync<Pharmacy>(
        //                            "spGetPagedPharmacyList",
        //                            parameters,
        //                            commandType: CommandType.StoredProcedure
        //                        )).ReturnsAsync(pharmacies);

        // Act
        var result = await _pharmacyRepository.GetPharmacyListPagedAsync(pagingInfo);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(pharmacies, Is.EqualTo(result.Data));
        Assert.That(pagingInfo, Is.EqualTo(result.PagingInfo));

        Assert.That(2, Is.EqualTo(result.Total));
        Assert.That(1, Is.EqualTo(result.Pages));
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnPharmacy()
    {
        // Arrange
        var pharmacyId = 1;
        var pharmacy   = new Pharmacy { Id = pharmacyId, Name = "Pharmacy 1" };

        //_dbConnectionMock.Setup(c => c.QueryFirstOrDefaultAsync<Pharmacy>(
        //                            "spGetPharmacyById",
        //                            new { Id = pharmacyId },
        //                            commandType: CommandType.StoredProcedure
        //                        )).ReturnsAsync(pharmacy);

        // Act
        var result = await _pharmacyRepository.GetByIdAsync(pharmacyId);

        // Assert
        Assert.That(pharmacy, Is.EqualTo(result));
    }

    [Test]
    public async Task InsertPharmacyAsync_ShouldReturnInsertedPharmacy()
    {
        // Arrange
        var pharmacy = new Pharmacy 
        { 
            Name                = "New Pharmacy", 
            Address             = "123 Main St", 
            City                = "City", 
            State               = "State", 
            Zip                 = "12345", 
            PrescriptionsFilled = 0,
            ModifiedBy          = "User" 
            
        };

        //_dbConnectionMock.Setup(c => c.QueryFirstOrDefaultAsync<Pharmacy>(
        //                            "spInsertPharmacy",
        //                            It.Is<object>(p => p.GetType().GetProperty("Name").GetValue(p).Equals(pharmacy.Name)
        //                                              && p.GetType().GetProperty("Address").GetValue(p).Equals(pharmacy.Address)
        //                                              && p.GetType().GetProperty("City").GetValue(p).Equals(pharmacy.City)
        //                                              && p.GetType().GetProperty("State").GetValue(p).Equals(pharmacy.State)
        //                                              && p.GetType().GetProperty("Zip").GetValue(p).Equals(pharmacy.Zip)
        //                                              && p.GetType().GetProperty("PrescriptionsFilled").GetValue(p).Equals(pharmacy.PrescriptionsFilled)
        //                                              && p.GetType().GetProperty("ModifiedBy").GetValue(p).Equals(pharmacy.ModifiedBy)
        //                            ),
        //                            commandType: CommandType.StoredProcedure
        //                        )).ReturnsAsync(pharmacy);

        // Act
        var result = await _pharmacyRepository.InsertPharmacyAsync(pharmacy);

        // Assert
        Assert.That(pharmacy, Is.EqualTo(result));
    }

    [Test]
    public async Task UpdatePharmacyAsync_ShouldReturnUpdatedPharmacy()
    {
        // Arrange
        var pharmacy = new Pharmacy { Id = 1, Name = "Updated Pharmacy", Address = "456 Main St", City = "City", State = "State", Zip = "12345", PrescriptionsFilled = 10, ModifiedBy = "User" };

        //_dbConnectionMock.Setup(c => c.QueryFirstOrDefaultAsync<Pharmacy>(
        //                            "spUpdatePharmacy",
        //                            It.Is<object>(p => p.GetType().GetProperty("Id").GetValue(p).Equals(pharmacy.Id)
        //                                              && p.GetType().GetProperty("Name").GetValue(p).Equals(pharmacy.Name)
        //                                              && p.GetType().GetProperty("Address").GetValue(p).Equals(pharmacy.Address)
        //                                              && p.GetType().GetProperty("City").GetValue(p).Equals(pharmacy.City)
        //                                              && p.GetType().GetProperty("State").GetValue(p).Equals(pharmacy.State)
        //                                              && p.GetType().GetProperty("Zip").GetValue(p).Equals(pharmacy.Zip)
        //                                              && p.GetType().GetProperty("PrescriptionsFilled").GetValue(p).Equals(pharmacy.PrescriptionsFilled)
        //                                              && p.GetType().GetProperty("ModifiedBy").GetValue(p).Equals(pharmacy.ModifiedBy)
        //                            ),
        //                            commandType: CommandType.StoredProcedure
        //                        )).ReturnsAsync(pharmacy);

        // Act
        var result = await _pharmacyRepository.UpdatePharmacyAsync(pharmacy);

        // Assert
        Assert.That(pharmacy, Is.EqualTo(result));
    }
}