//version:v1.0
using System;
using System.Collections.Generic;
using System.Globalization;

namespace 随机点名系统
{
    /// <summary>
    /// Stores the weight (the chance to be picked) of every name and performs the weighted random pick.
    /// A weight is a whole number in 1..MaxWeight; 1 means "normal chance" and an entry that has no weight yet also gets 1,
    /// so a brand new name list behaves exactly like the unweighted pick. A weight of 0 disables a name.
    /// The class knows nothing about Windows Forms, so the arithmetic can be tested on its own.
    /// </summary>
    /// <remarks>
    /// Public rather than internal only because the public constructor of the public Settings form takes one; nothing
    /// outside this assembly uses it.
    /// </remarks>
    public sealed class WeightStore
    {
        /// <summary>
        /// Upper bound of one weight. It keeps the percentage readable and prevents an accidental 999999 from
        /// swallowing every other name.
        /// </summary>
        public const int MaxWeight = 100;

        private readonly Dictionary<string, int> _weights = new Dictionary<string, int>(StringComparer.Ordinal);

        /// <summary>
        /// The weight of a name; 1 when the name has never been adjusted. Unknown names are not added to the dictionary.
        /// </summary>
        public int GetWeight(string name)
        {
            int weight;
            if (_weights.TryGetValue(name, out weight))
            {
                return weight;
            }

            return 1;
        }

        /// <summary>
        /// Sets the weight of a name. Values below 0 are stored as 0 and values above <see cref="MaxWeight"/> are capped,
        /// so a corrupted input can never break the pick.
        /// </summary>
        public void SetWeight(string name, int weight)
        {
            int value = weight;
            if (value < 0)
            {
                value = 0;
            }
            else if (value > MaxWeight)
            {
                value = MaxWeight;
            }

            _weights[name] = value;
        }

        /// <summary>
        /// Sum of the weights of the given names. Names that are not in the list are ignored, so the total always matches
        /// what the pick can actually select.
        /// </summary>
        public long TotalWeight(IList<string> names)
        {
            long total = 0;
            for (int i = 0; i < names.Count; i++)
            {
                total += GetWeight(names[i]);
            }

            return total;
        }

        /// <summary>
        /// The chance of one name as a percentage of the total, rounded to one decimal (for example 12.5).
        /// A name with weight 0, an empty list and a null list all return 0.
        /// </summary>
        public double PercentOf(string name, IList<string> names)
        {
            long total = TotalWeight(names);
            if (total <= 0)
            {
                return 0.0;
            }

            // One decimal place, the same rounding the dialog shows.
            return Math.Round(GetWeight(name) * 100.0 / total, 1);
        }

        /// <summary>
        /// Picks one name at random, where a name with weight W is W times as likely as a name with weight 1.
        /// Returns null when every name has weight 0 (or the list is empty), so the caller can show a message instead of crashing:
        /// Random.Next(0) would throw.
        /// </summary>
        public string Pick(IList<string> names, Random random)
        {
            return Roll(names, random, null);
        }

        /// <summary>
        /// Picks one name at random, skipping the names in <paramref name="blocked"/> (for example the person who was just
        /// called). The weights of the remaining names are scaled, so their relative chances stay the same.
        /// The blocked names are only ignored when at least one name outside the set has a weight above 0; otherwise the
        /// full list is used, because "nobody may be called twice in 30 seconds" must not turn into "nobody can be called".
        /// </summary>
        public string Pick(IList<string> names, Random random, ICollection<string> blocked)
        {
            if (blocked == null || blocked.Count == 0)
            {
                return Roll(names, random, null);
            }

            long allowedTotal = 0;
            for (int i = 0; i < names.Count; i++)
            {
                if (!blocked.Contains(names[i]))
                {
                    allowedTotal += GetWeight(names[i]);
                }
            }

            if (allowedTotal > 0)
            {
                return Roll(names, random, blocked);
            }

            // Everyone is blocked (for example a one-person class), so the block cannot be honoured.
            return Roll(names, random, null);
        }

        /// <summary>
        /// The actual roulette. Names in <paramref name="blocked"/> are skipped while the running total is computed, so a
        /// blocked name never uses up any of the rolled range and the remaining names keep their relative chances.
        /// </summary>
        private string Roll(IList<string> names, Random random, ICollection<string> blocked)
        {
            long total = 0;
            for (int i = 0; i < names.Count; i++)
            {
                if (blocked == null || !blocked.Contains(names[i]))
                {
                    total += GetWeight(names[i]);
                }
            }

            if (total <= 0)
            {
                return null;
            }

            // Random.Next only accepts an int upper bound; a name list that large cannot be typed in practice.
            // The NextDouble branch keeps the pick correct if it ever happens.
            long roll;
            if (total <= int.MaxValue)
            {
                roll = random.Next((int)total);
            }
            else
            {
                roll = (long)(random.NextDouble() * total);
            }

            long cumulative = 0;
            for (int i = 0; i < names.Count; i++)
            {
                if (blocked != null && blocked.Contains(names[i]))
                {
                    continue;
                }

                cumulative += GetWeight(names[i]);
                if (roll < cumulative)
                {
                    return names[i];
                }
            }

            // Only reachable through a corrupted total; fall back to the last positive entry instead of returning null.
            for (int i = names.Count - 1; i >= 0; i--)
            {
                if ((blocked == null || !blocked.Contains(names[i])) && GetWeight(names[i]) > 0)
                {
                    return names[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Formats a weight as one decimal number with the invariant culture, so the text never depends on the Windows
        /// regional settings (for example a Persian or Arabic decimal separator).
        /// </summary>
        public static string FormatWeight(int weight)
        {
            return weight.ToString("0.0", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Formats a percentage with the invariant culture, so "12.5" is shown the same on every Windows language.
        /// </summary>
        public static string FormatPercent(double percent)
        {
            return percent.ToString("0.0", CultureInfo.InvariantCulture) + "%";
        }
    }
}
