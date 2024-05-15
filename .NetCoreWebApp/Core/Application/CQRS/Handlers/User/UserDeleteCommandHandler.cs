using Application.CQRS.Commands;
using Application.Interfaces;
using AutoMapper;
using Common.Interfaces;
using Domain.Entities.Aggregates;
using MediatR;

namespace Application.CQRS.Handlers.User
{
    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommandRequest, Unit>
    {
        private readonly IWebApiIuow _unitOfWork;

        public UserDeleteCommandHandler(IWebApiIuow unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UserDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userRepo = _unitOfWork.GetRepository<AppUser>();

                var user = await userRepo.GetByIdAsync(request.Id);

                if (user != null)
                {
                    userRepo.Remove(user);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete user with ID {request.Id}.", ex);
            }
            return Unit.Value;
        }
    }
}
