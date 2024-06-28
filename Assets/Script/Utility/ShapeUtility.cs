namespace Game.Utility
{
    public static class ShapeUtility
    {
        public static int ShapeSize(this EShape shape)
        {
            switch (shape)
            {
                case EShape.One: return 1;
                case EShape.Horizontal: return 2;
                case EShape.Vertical: return 2;
                case EShape.Square: return 4;
            }

            return 0;
        }
    }
}