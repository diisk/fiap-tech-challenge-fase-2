using AutoMapper;

namespace Application.Mappers
{
    public abstract class CustomMapper<S, T>
    {
        private readonly IMapper mapper;

        protected CustomMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public void Map(S source, T target)
        {
            mapper.Map(source, target);
            ApplyCustomMappings(source, target);
        }

        public T Map(S source)
        {
            T target = mapper.Map<T>(source);
            ApplyCustomMappings(source, target);

            return target;
        }


        protected virtual void ApplyCustomMappings(S source, T target) { }


    }
}
