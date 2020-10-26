using System.Collections.Generic;
using System.Threading.Tasks;
using TestApriorit.Core;
using TestApriorit.Core.Models;
using TestApriorit.Core.Services;

namespace TestApriorit.Services
{
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PositionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Position> CreatePosition(Position newPosition)
        {
            await _unitOfWork.Positions.AddAsync(newPosition);
            await _unitOfWork.CommitAsync();

            return newPosition;
        }

        public async Task DeletePosition(Position position)
        {
            _unitOfWork.Positions.Remove(position);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Position>> GetAllPositions()
        {
            return await _unitOfWork.Positions.GetAllAsync();
        }

        public async Task<Position> GetPositionById(int id)
        {
            return await _unitOfWork.Positions.GetByIdAsync(id);
        }

        public async Task UpdatePosition(Position positionToBeUpdated, Position position)
        {
            positionToBeUpdated.Title = position.Title;

            await _unitOfWork.CommitAsync();
        }
    }
}
