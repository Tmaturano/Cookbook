using AutoMapper;

namespace Cookbook.Application.Services.AutoMapper;

public class AutoMapperConfiguration
{
    protected AutoMapperConfiguration()
    {
    }

    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(config =>
        {
            config.AddProfile(new ResponseToDomainMappingProfile());
            config.AddProfile(new DomainToResponseMappingProfile());
        });
    }
}
