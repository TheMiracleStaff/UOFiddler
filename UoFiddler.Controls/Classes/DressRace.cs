namespace UoFiddler.Controls.Classes
{
    /// <summary>
    /// A race selectable in the Dress tab: its display name and the gumps drawn as the paperdoll body.
    /// </summary>
    public sealed class DressRace
    {
        public DressRace(string name, int maleGump, int femaleGump)
        {
            Name = name;
            MaleGump = maleGump;
            FemaleGump = femaleGump;
        }

        public string Name { get; }

        public int MaleGump { get; }

        public int FemaleGump { get; }

        public const int Human = 0;
        public const int Elf = 1;
        public const int Gargoyle = 2;
    }
}
