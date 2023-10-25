using System;
using DomeGym.Domain.Interfaces;

namespace DomeGym.Domain.UnitTests.TestUtils.Services;

public class TestDateTimeProvider : IDateTimeProvider
{
    private readonly DateTime? _dateTime;

    public TestDateTimeProvider(DateTime? dateTime = null)
    {
        _dateTime = dateTime;
    }

    public DateTime UtcNow => _dateTime ?? DateTime.UtcNow;
}
