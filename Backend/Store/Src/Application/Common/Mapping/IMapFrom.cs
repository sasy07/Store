using AutoMapper;

namespace Application.Common.Mapping;

public interface IMapFrom<T>
{
    virtual void Mapping(Profile profile) 
        => profile.CreateMap(typeof(T), GetType());
}