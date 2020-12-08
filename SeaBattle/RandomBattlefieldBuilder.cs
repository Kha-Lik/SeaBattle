using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SeaBattle
{
    public class RandomBattlefieldBuilder : BattlefieldBuilder
    {
        private IBattlefield _battlefield;

        public RandomBattlefieldBuilder(GameSettings settings) : base(settings)
        {
        }

        public override void BuildBattlefield()
        {
            if (Settings.FieldSize <= 0) 
                throw new BattlefieldBuilderException($"Field size must be greater than zero, but is {Settings.FieldSize}");
            _battlefield = new Battlefield(Settings.FieldSize);

            for (var i = 0; i < Settings.FieldSize; i++)
            {
                for (var j = 0; j < Settings.FieldSize; j++)
                {
                    Point point = new(j, i);

                    _battlefield[point] = new Cell(point);
                }
            }

            _battlefield.Ships = new();
        }

        public override void PlaceShips()
        {
            if (_battlefield is null)
                throw new BattlefieldBuilderException(nameof(_battlefield) ,"Battlefield must be created before placing ships");

            if (IsShipsDensityTooHigh()) throw new BattlefieldBuilderException("Given settings will cause too high ships density");
            
            var ships = GetShipsFromSettings();
            
            foreach (var (size, count) in ships)
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
        private bool IsShipsDensityTooHigh()
        {
            const double highestAvailibleDensityCoef = 1.21;
            var shipsAmount = GetShipsFromSettings();

            var decksCount = shipsAmount.Sum(x => x.Key * x.Value);
            var maxNeighbourCellsCount = shipsAmount.Sum(x => (x.Key * 2 + 6) * x.Value);
            var densityCoef = (decksCount + maxNeighbourCellsCount) / Math.Pow(Settings.FieldSize, 2);

            return densityCoef > highestAvailibleDensityCoef;
        }
        private IList<Cell> GetEmptyCells()
        {
            return _battlefield.Cast<Cell>().Where(c => 
                c.State.HasFlag(CellState.Empty) && !c.State.HasFlag(CellState.NearShip)).ToList();
        }

        private bool IsRangeOfCellSuitable(ICollection<Cell> range, IEnumerable<Cell> emptyCells)
        {
            return range.All(emptyCells.Contains);
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