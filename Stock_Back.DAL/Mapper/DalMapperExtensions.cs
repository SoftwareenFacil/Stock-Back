using AutoMapper;
using System.Reflection;

namespace Stock_Back.DAL.Mapper
{
    public static class DalMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> MapDirty<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression)
        {
            mappingExpression.ForAllMembers(memberOptions =>
            {
                memberOptions.Condition((src, dest, srcMember, destMember) =>
                {
                    if (srcMember is PropertyInfo propertyInfo)
                    {
                        var sourceValue = propertyInfo.GetValue(src);
                        var destinationValue = propertyInfo.GetValue(dest);
                        return !Equals(sourceValue, destinationValue);
                    }
                    return true;
                });
            });

            return mappingExpression;
        }
    }
}
