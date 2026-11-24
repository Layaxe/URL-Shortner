using Microsoft.Extensions.Options;

namespace URLShortner.Services
{
    public class SnowflakeIdGenerator : IIdGenerator
    {
        private const long EpochMs = 1767225600000; // January 1, 2026, at 00:00:00 UTC
        private const int SequenceBits = 12;
        private const int WorkerIdBits = 10;
        private const long MaxSequence = (1L << SequenceBits) - 1; // 4095
        private const long MaxWorkerId = (1L << WorkerIdBits) - 1; // 1023
        private const int WorkerIdShift = SequenceBits; // Sequence bits
        private const int TimestampShift = SequenceBits + WorkerIdBits; // Sequence bits + WorkerId bits

        private readonly TimeProvider _timeProvider;
        private readonly long _workerId;
        private readonly Lock _lock = new();
        private long _lastTimestamp = -1; // The value is -1 so you know it's the first
        private long _sequence;

        public SnowflakeIdGenerator(IOptions<SnowflakeIdOptions> options, TimeProvider timeProvider) {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(timeProvider);

            var workerId = options.Value.WorkerId;

            ArgumentOutOfRangeException.ThrowIfNegative(workerId);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(workerId, MaxWorkerId);
            
            _workerId = workerId;
            _timeProvider = timeProvider;
        }

        public long CreateId()
        {
            lock (_lock) // Ensure thread safety so no more than one thread read and access the same data
            {
                var currentTimestamp = _timeProvider.GetUtcNow().ToUnixTimeMilliseconds() - EpochMs; // Todo Method1 here

                if (currentTimestamp < _lastTimestamp) // Back in time
                {
                    throw new InvalidOperationException($"Clock moved backwards. Refusing to generate ID for {_lastTimestamp - currentTimestamp} milliseconds.");
                }

                if (currentTimestamp == _lastTimestamp) // Current Time
                {
                    _sequence = (_sequence + 1) & MaxSequence; // Adds 1 then using bitwise operation to compare value if the values equal to MaxSequence the result is 0.

                    if (_sequence == 0)
                    {
                        currentTimestamp = _timeProvider.GetUtcNow().ToUnixTimeMilliseconds() - EpochMs; // Todo Method1 here
                        while (currentTimestamp <= _lastTimestamp) // Todo Method2 here
                        {
                            currentTimestamp = _timeProvider.GetUtcNow().ToUnixTimeMilliseconds() - EpochMs; // Todo Method1 here
                        }
                    }
                }
                else // The future baby 
                {
                    _sequence = 0L;
                }

                _lastTimestamp = currentTimestamp;

                return (currentTimestamp << TimestampShift) | (_workerId << WorkerIdShift) | (_sequence);
            }
        }
    }
}
