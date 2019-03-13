namespace Labirint.MazeSolvers
{
    internal class MazeMove
    {
        public MazeMove(Direction direction, Frame frame)
        {
            Direction = direction;
            Frame = frame;
        }

        public Direction Direction { get; }
        public Frame Frame { get; }

        protected bool Equals(MazeMove other)
        {
            return Direction == other.Direction &&
                   Frame == other.Frame
                ;
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MazeMove) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Direction * 397) ^ Frame.GetHashCode();
            }
        }

        public static bool operator ==(MazeMove left, MazeMove right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MazeMove left, MazeMove right)
        {
            return !Equals(left, right);
        }
    }
}