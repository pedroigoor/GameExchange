using GameExchange.Application.UseCases.Platform;
using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.Category;
using GameExchange.Excptions.ExceptionBase;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.Category.Register
{
    public class SaveCategoryUseCase(ICategoryWriteOnlyRepository categoryWriteOnlyRepository, IUnitOfWork unitOfWork) : ISaveCategoryUseCase
    {
        private readonly ICategoryWriteOnlyRepository _categoryWriteOnlyRepository = categoryWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ResponseCategoryJson> Execute(RequestCategory request)
        {
            await Validate(request);

            var category = request.Adapt<Domain.Entities.Category>();

            await _categoryWriteOnlyRepository.Add(category);

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
