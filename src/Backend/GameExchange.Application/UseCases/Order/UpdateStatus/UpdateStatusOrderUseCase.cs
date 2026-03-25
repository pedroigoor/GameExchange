using GameExchange.Application.UseCases.Listing.ChangeStatus;
using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Enum;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.Order;
using GameExchange.Excptions;
using GameExchange.Excptions.ExceptionBase;
using Mapster;

namespace GameExchange.Application.UseCases.Order.UpdateStatus
{
    public class UpdateStatusOrderUseCase (IOrderUpdateOnlyRepository orderUpdateOnlyRepository,
        IUnitOfWork unitOfWork) : IUpdateStatusOrderUseCase
    {
        private readonly IOrderUpdateOnlyRepository _orderUpdateOnlyRepository = orderUpdateOnlyRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<ResponseOrderJson> Execute(long idOrder, RequestChangeStatusOrder status)
        {
            await ValidateRequest(status);

            var order = await _orderUpdateOnlyRepository.GetById(idOrder) ?? throw new NotFoundException(ResourceMessagesException.RESOURCE_NOT_FOUND);

            order.Status = (OrderStatus)status.Status;

            _orderUpdateOnlyRepository.Update(order);

            await _unitOfWork.Commit();

            return order.Adapt<ResponseOrderJson>();
        }

        private static async Task ValidateRequest(RequestChangeStatusOrder status)
        {
            var validator = new UpdateStatusValidator();
            var validationResult = validator.Validate(status);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
