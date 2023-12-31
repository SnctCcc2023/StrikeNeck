﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strikeneck.AppSetting
{
    public class DetectionSensitivity
    {
        private const int MAX_SENSITIVITY = 100;
        private const int MIN_SENSITIVITY = -100;
        public readonly int sensitivity;

        public DetectionSensitivity(int sensitivity = 0)
        {
            if (MAX_SENSITIVITY < sensitivity) throw new ArgumentOutOfRangeException($"Sensitivity must be between {MIN_SENSITIVITY} and {MAX_SENSITIVITY}");
            if (sensitivity < MIN_SENSITIVITY) throw new ArgumentOutOfRangeException($"Sensitivity must be between {MIN_SENSITIVITY} and {MAX_SENSITIVITY}");
            this.sensitivity = sensitivity;
        }
    }
}
