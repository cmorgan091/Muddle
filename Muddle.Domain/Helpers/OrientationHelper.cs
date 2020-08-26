using System;
using Muddle.Domain.Models;

namespace Muddle.Domain.Helpers
{
    public static class OrientationHelper
    {
        public static Char CharRepresentation(this Orientations orientation)
        {
            switch (orientation)
            {
                case Orientations.Horizontal:
                    return '-';
                case Orientations.Vertical:
                    return '|';
                case Orientations.Both:
                    return '+';
            }

            throw new Exception($"Unknown orientation type '{orientation}' sent to {nameof(CharRepresentation)}");
        }

        public static Char CharRepresentation(this Orientations? orientation)
        {
            if (!orientation.HasValue)
            {
                return ' ';
            }

            return orientation.Value.CharRepresentation();
        }
    }
}