using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SeaBattle
{
    public class RandomBattlefieldBuilder : BattlefieldBuilder
    {
        private readonly Dictionary<int, int> _ships;
        private IBattlefield _battlefield;
        
        public RandomBattlefieldBuilder(GameSettings settings) : base(settings)
        {
            _ships = GetShipsFromSettings();
        }

        public override void BuildBattlefield()
        {
            _battlefield = new Battlefield(Settings.FieldSize);

            for (var i = 0; i < Settings.FieldSize; i++)
            {
                for (var j = 0; j < Settings.FieldSize; j++)
                {
                    Point point = new(j, i);

                    _battlefield[point] = new Cell(point);
                }
            }
        }

        public override void PlaceShips()
        {
            foreach (var (size, count) in _ships)
            {
                for (int i = 0; i < count; i++)
                {
                    PlaceShipRandomly(size);
                }
            }
        }

        public override IBattlefield GetResult()
        {
            return _battlefield;
        }

        private void PlaceShipRandomly(int shipSize)
        {
            var random = RandomHelper.GetHelper();
            
            var emptyCells = GetEmptyCells();
            var randomCell = random.GetRandomCell(emptyCells);
            var isVertical = random.GetRandomBool();
            
            List<Cell> range = new();

            do {
                var pointRange = GetPointRange(randomCell.Coordinates, shipSize, isVertical);
                //if some points are outside of the field,
                //get new start point, new random direction and skip iteration
                if (!pointRange.All(p => _battlefield.IsPointInField(p)))
                {
                    randomCell = random.GetRandomCell(emptyCells);
                    isVertical = random.GetRandomBool();
                    continue;
                } 

                range.Clear();
                range.AddRange(pointRange.Select(point => _battlefield[point]));
            } while (!IsRangeOfCellSuitable(range, emptyCells));
            
            foreach (var cell in range) 
                cell.State = CellState.Ship;
            _battlefield.Ships.Add(new(range));
            
            EncircleShip(range);
        }

        private IList<Cell> GetEmptyCells()
        {
            return _battlefield.Cast<Cell>().Where(c => 
                c.State.HasFlag(CellState.Empty) && !c.State.HasFlag(CellState.NearShip)).ToList();
        }

        private bool IsRangeOfCellSuitable(ICollection<Cell> range, IEnumerable<Cell> emptyCells)
        {
            return emptyCells.Intersect(range).Equals(range);
        }

        private ICollection<Point> GetPointRange(Point startPoint, int size, bool isVertical)
        {
            List<Point> points = new();

            for (var i = 0; i < size; i++)
            {
                var point = isVertical
                    ? new Point
                    {
                        X = startPoint.X,
                        Y = startPoint.Y + i
                    }
                    : new Point
                    {
                        X = startPoint.X + i,
                        Y = startPoint.Y
                    };
                
                points.Add(point);
            }

            return points;
        }

        private void EncircleShip(ICollection<Cell> range)
        {
            List<Cell> neighs = new();

            foreach (var cell in range)
            {
                neighs.AddRange(_battlefield.GetNeighbours(cell).Where(c => c.State is CellState.Empty ));
            }

            neighs = neighs.Distinct().ToList();

            foreach (var cell in neighs)
                cell.State = CellState.NearShip;
        }
    }
}