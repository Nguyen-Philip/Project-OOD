using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public interface IEntity
    {
        string Name { set; get; }
        Room Location { set; get; }

    }
}
