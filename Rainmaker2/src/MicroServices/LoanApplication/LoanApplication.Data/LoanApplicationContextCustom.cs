using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoanApplicationDb.Data
{
    public class CustomerSearch
    {
        public string label { get; set; }
        public string Ids { get; set; }
        public string StateName { get; set; }
    }
    public class LocationSearch
    {
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int? CountyId { get; set; }
        public string CountyName { get; set; }
        public string ZipPostalCode { get; set; }
        public string Abbreviation { get; set; }
    }
    public partial class LoanApplicationContext : DbContext
    {
        public IQueryable<CustomerSearch> CustomerSearchByZipcode(int zipCode) => Set<CustomerSearch>().FromSqlInterpolated($"exec dbo.CustomerSearchByZipcode @zipCode = {zipCode}");
        public IQueryable<CustomerSearch> CustomerSearchByString(string searchKey) => Set<CustomerSearch>().FromSqlInterpolated($"exec dbo.CustomerSearchByString @searchKey = {searchKey}");
        public IQueryable<LocationSearch> LocationSearchByCityCountyStateZipCode(string cityName, string stateName, string countyName, string zipCode) => Set<LocationSearch>().FromSqlInterpolated($"exec dbo.LocationSearch @ZipCode = {zipCode}, @StateName = {stateName}, @CityName = {cityName}, @CountyName = {countyName}");
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerSearch>().HasNoKey().ToView(null);
            modelBuilder.Entity<LocationSearch>().HasNoKey().ToView(null);
        }
    }
}
