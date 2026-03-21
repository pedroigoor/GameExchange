using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.Category;
using GameExchange.Excptions;
using GameExchange.Excptions.ExceptionBase;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.Category.Update
{
    public class UpdateCategoryUseCase(ICategoryUpdateOnlyRepository categoryUpdateOnlyRepository, IUnitOfWork unitOfWork) : IUpdateCategoryUseCase
    {
        private readonly ICategoryUpdateOnlyRepository _categoryUpdateOnlyRepository = categoryUpdateOnlyRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ResponseCategoryJson> Execute(long id, RequestCategory request)
        {
            await Validate(request);
            var category = await _categoryUpdateOnlyRepository.GetById(id) ?? throw new NotFoundException(ResourceMessagesException.RESOURCE_NOT_FOUND);

            request.Adapt(category);

            _categoryUpdateOnlyRepository.Update(category);

            await _unitOfWork.Commit();

            return category.Adapt<ResponseCategoryJson>();

            
        }


        private static async Task Validate(RequestCategory request)
        {
            var validator = new CategoryValidator();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
