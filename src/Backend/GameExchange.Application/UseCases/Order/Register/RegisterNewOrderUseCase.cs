using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Entities;
using GameExchange.Domain.Enum;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.Listing;
using GameExchange.Domain.Repositories.Order;
using GameExchange.Domain.Services;
using GameExchange.Excptions;
using GameExchange.Excptions.ExceptionBase;
using GameExchange.Infrastructe.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.Order.Register
{
    public class RegisterNewOrderUseCase(IOrderWriteOnlyRepository orderWriteOnlyRepository,
                                         IListingReadOnlyRepository listingReadOnlyRepository,
                                         ILoggedUser loggedUser,
                                         IUnitOfWork UnitOfWork) : IRegisterNewOrderUseCase
    {
        private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository = orderWriteOnlyRepository;
        private readonly IListingReadOnlyRepository _listingReadOnlyRepository = listingReadOnlyRepository;

        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IUnitOfWork _unitOfWork = UnitOfWork;
        public async Task<ResponseOrderJson> Execute(long listingId)
        {
            var user = await _loggedUser.User();
            var listing = await _listingReadOnlyRepository.GetById(listingId) ?? throw new NotFoundException(ResourceMessagesException.RESOURCE_NOT_FOUND);

            if (!listing.Status.Equals(ListingStatus.Active))
                throw new ErrorOnValidationException(ResourceMessagesException.LISTING_NOT_ACTIVE);

            if (listing.SellerId == user.Id)
                throw new ErrorOnValidationException(ResourceMessagesException.NOT_BUY_SELF_LISTING);

            var order = new Domain.Entities.Order(user.Id, listingId, listing.Price);

            await _orderWriteOnlyRepository.Add(order);

            await _unitOfWork.Commit();

           
            return new ResponseOrderJson
            {
                OrderID = order.Id
            }; ;
        }
    }
}
