using System.Reflection;
using AutoMapper;
using Krosoft.Extensions.Core.Models;
using Krosoft.Extensions.Mapping.Extensions;
using Krosoft.Extensions.Samples.Library.Mappings;
using Krosoft.Extensions.Samples.Library.Models;
using Krosoft.Extensions.Samples.Library.Models.Dto;
using Krosoft.Extensions.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Mapping.Tests.Extensions;

[TestClass]
public class EnumerableExtensionsTests : BaseTest
{
    private IMapper _mapper = null!;

    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        var all = new List<Assembly> { typeof(CompteProfile).Assembly };
        services.AddAutoMapper(all);
    }

    [TestMethod]
    public void MapToTest()
    {
        var source = new List<Compte>
        {
            new Compte { Name = "Test_1" },
            new Compte { Name = "Test" }
        };

        var dest = source.MapTo<Compte, CompteDto>(_mapper).ToList();

        Check.That(dest).HasSize(2);
        Check.That(dest.Select(x => x.Name)).ContainsExactly("Test_1", "Test");
    }

    [TestMethod]
    public void ToPaginationTest()
    {
        var source = new List<Compte>
        {
            new Compte { Name = "Test_1" },
            new Compte { Name = "Test" }
        };

        var dest = source.ToPagination<Compte, CompteDto>(new PaginationRequest(), _mapper);
    }

    [TestInitialize]
    public void SetUp()
    {
        var serviceProvider = CreateServiceCollection();
        _mapper = serviceProvider.GetRequiredService<IMapper>();
    }
}