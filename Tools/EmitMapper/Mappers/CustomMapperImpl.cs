using System;
using EmitMapper.MappingConfiguration.MappingOperations;

namespace EmitMapper.Mappers
{
    internal abstract class CustomMapperImpl: ObjectsMapperBaseImpl
    {
        public CustomMapperImpl(
            EmitMapperManager mapperMannager, 
            Type TypeFrom, 
            Type TypeTo, 
            IMappingConfigurator mappingConfigurator,
			object[] storedObjects)
        {
			Initialize(mapperMannager, TypeFrom, TypeTo, mappingConfigurator, storedObjects);
        }
    }
}