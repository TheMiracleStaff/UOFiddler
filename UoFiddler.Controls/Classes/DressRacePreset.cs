using System;
using System.Collections.Generic;
using System.Linq;

namespace UoFiddler.Controls.Classes
{
    /// <summary>
    /// A set of the three Dress tab races (human, elf, gargoyle slot) with their paperdoll body gumps.
    /// Shards that replace the body gumps, or a race, get their own preset.
    /// </summary>
    public sealed class DressRacePreset
    {
        private DressRacePreset(string key, string name, DressRace human, DressRace elf, DressRace gargoyle)
        {
            Key = key;
            Name = name;
            Races = new[] { human, elf, gargoyle };
        }

        /// <summary>
        /// Identifier stored in the profile.
        /// </summary>
        public string Key { get; }

        public string Name { get; }

        /// <summary>
        /// Indexed by <see cref="DressRace.Human"/>, <see cref="DressRace.Elf"/> and <see cref="DressRace.Gargoyle"/>.
        /// </summary>
        public IReadOnlyList<DressRace> Races { get; }

        /// <summary>
        /// Body gumps of the standard client.
        /// </summary>
        public static DressRacePreset Default { get; } = new DressRacePreset("default", "Default",
            new DressRace("Human", 0xC, 0xD),
            new DressRace("Elf", 0xE, 0xF),
            new DressRace("Gargoyle", 0x29A, 0x299));

        /// <summary>
        /// The Miracle shard: custom body gumps, with dwarves in the gargoyle slot.
        /// </summary>
        public static DressRacePreset TheMiracle { get; } = new DressRacePreset("themiracle", "The Miracle",
            new DressRace("Umano", 0xC4E2, 0xC4E3),
            new DressRace("Elfo", 0xC735, 0xC736),
            new DressRace("Nano", 0xC508, 0xC509));

        public static IReadOnlyList<DressRacePreset> All { get; } = new[] { Default, TheMiracle };

        /// <summary>
        /// Returns the preset with the given key, or <see cref="TheMiracle"/>, this build's default, when there is none.
        /// </summary>
        public static DressRacePreset Find(string key)
        {
            return All.FirstOrDefault(p => string.Equals(p.Key, key, StringComparison.OrdinalIgnoreCase)) ?? TheMiracle;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
