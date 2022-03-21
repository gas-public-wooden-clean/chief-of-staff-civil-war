using System;
using System.Collections.Generic;

namespace ChiefOfStaffCivilWar
{
	public class WeightedOutcomes<T>
	{
		public WeightedOutcomes()
		{
			_totalWeight = 0;
			_weights = new List<WeightedOutcome>();
		}

		double _totalWeight;
		readonly IList<WeightedOutcome> _weights;

		public bool IsEmpty => _weights.Count <= 0;

		public void Add(T item, double weight)
		{
			_totalWeight += weight;
			_weights.Add(new WeightedOutcomeLeaf(item, _totalWeight));
		}

		public void Add(WeightedOutcomes<T> group, double weight)
		{
			_totalWeight += weight;
			_weights.Add(new WeightedOutcomeNested(group, weight));
		}

		public T Get(Random random)
		{
			double value = random.NextDouble() * _totalWeight;

			foreach (WeightedOutcomeLeaf outcome in _weights)
			{
				if (outcome.Weight > value)
				{
					return outcome.Get(random);
				}
			}

			throw new InvalidOperationException();
		}

		abstract class WeightedOutcome
		{
			public WeightedOutcome(double weight) => Weight = weight;

			public abstract T Get(Random generator);

			public double Weight
			{
				get;
				private set;
			}
		}

		class WeightedOutcomeLeaf : WeightedOutcome
		{
			public WeightedOutcomeLeaf(T outcome, double weight)
				: base(weight) => _outcome = outcome;

			readonly T _outcome;

			public override T Get(Random generator)
			{
				return _outcome;
			}
		}

		class WeightedOutcomeNested : WeightedOutcome
		{
			public WeightedOutcomeNested(WeightedOutcomes<T> wrapped, double weight)
				: base(weight) => _wrapped = wrapped;

			readonly WeightedOutcomes<T> _wrapped;

			public override T Get(Random generator)
			{
				return _wrapped.Get(generator);
			}
		}
	}
}
