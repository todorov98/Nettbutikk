using System;
using System.Threading.Tasks;
using Nettbutikk.Data.DTO;
using Nettbutikk.Models;

namespace Nettbutikk.Data.Services
{
    public class DtoMapperService
    {
        public DtoMapperService()
        {

        }

        /// <summary>
        /// Maps the incoming DTO object to the correct entity object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dto"></param>
        /// <returns>Entity object for the incoming DTO.</returns>
        public Task<TEntity> MapFromDTO<TEntity, TDto>(TDto dto)
            where TDto : IDto
            where TEntity : IEntity
        {
            if (dto is null)
                throw new ArgumentNullException("Cannot map object that is null.");

            TEntity entity;
            var typeName = typeof(TEntity).ToString();
            try
            {
                entity = (TEntity)Activator.CreateInstance(Type.GetType(typeName));
            }

            catch (Exception)
            {
                throw new Exception("Entity object creation failed. Control input data.");
            }

            foreach (var dtoProperty in dto.GetType().GetProperties())
            {
                foreach (var entityProperty in entity.GetType().GetProperties())
                {
                    if (entityProperty.Name.Equals(dtoProperty.Name) 
                        && entityProperty.PropertyType == dtoProperty.PropertyType)
                    {
                        var value = dtoProperty.GetValue(dto);
                        entityProperty.SetValue(entity, value);
                        break;
                    }
                }
            }

            return Task.FromResult(entity);
        }

        /// <summary>
        /// Maps an incoming entity object to the correct DTO object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>DTO object for the incoming entity.</returns>
        public Task<TDto> MapToDTO<TEntity, TDto>(TEntity entity) 
            where TDto : IDto
            where TEntity : IEntity
        {
            if (entity is null)
                throw new ArgumentNullException("Cannot map object that is null.");

            TDto dto;
            var dtoTypeName = typeof(TDto).ToString();
            try
            {
                dto = (TDto)Activator.CreateInstance(Type.GetType(dtoTypeName));
            }

            catch (Exception)
            {
                throw new Exception("DTO object creation failed. Control input data.");
            }

            foreach (var entityProperty in entity.GetType().GetProperties())
            {
                foreach (var dtoProperty in dto.GetType().GetProperties())
                {
                    if (dtoProperty.Name.Equals(entityProperty.Name) 
                        && dtoProperty.PropertyType == entityProperty.PropertyType)
                    {
                        var value = entityProperty.GetValue(entity);
                        dtoProperty.SetValue(dto, value);
                        break;
                    }
                }
            }

            return Task.FromResult(dto);
        }
    }
}
