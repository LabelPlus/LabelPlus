namespace LabelPlus
{
    public abstract class Command
    {
        #region methods
        public abstract void Excute();
        public abstract void Undo();

        public override string ToString()
        {
            return "This is a command without info";
        }
        #endregion
    }
}
