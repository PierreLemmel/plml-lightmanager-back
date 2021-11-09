﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LightManager.Logic.Dmx512
{
    public record FixtureMode(string Name, IReadOnlyDictionary<int, ChannelType> Chans);

    public record FixtureModel(string Name, IReadOnlyDictionary<int, FixtureMode> Modes);
}