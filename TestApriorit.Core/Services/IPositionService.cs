using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApriorit.Core.Models;

namespace TestApriorit.Core.Services
{
    public interface IPositionService
    {
        Task<IEnumerable<Position>> GetAllPositions();
        Task<Position> GetPositionById(int id);
        Task<Position> CreatePosition(Position newPosition);
        Task UpdatePosition(Position positionToBeUpdated, Position position);
        Task DeletePosition(Position position);
    }
}
