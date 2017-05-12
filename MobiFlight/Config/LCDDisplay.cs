﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MobiFlight.Config
{
    public class LCDDisplay : BaseDevice
    {
        const ushort _paramCount = 6;
        [XmlAttribute]
        public String DinPin = "-1";
        [XmlAttribute]
        public String ClsPin = "-2";
        [XmlAttribute]
        public String ClkPin = "-3";
        [XmlAttribute]
        public Byte Brightness = 15;
        [XmlAttribute]
        public String NumModules = "1";

        public LCDDisplay() { Name = "LCDDisplay"; _type = DeviceType.LedModule; }

        override public String ToInternal()
        {
            throw new NotImplementedException();
            return base.ToInternal() + Separator
                 + DinPin + Separator                 
                 + ClsPin + Separator
                 + ClkPin + Separator
                 + Brightness + Separator
                 + NumModules + Separator
                 + Name + End;
        }

        override public bool FromInternal(String value)
        {
            throw new NotImplementedException();
            if (value.Length == value.IndexOf(End) + 1) value = value.Substring(0, value.Length - 1);
            String[] paramList = value.Split(Separator);
            if (paramList.Count() != _paramCount + 1)
            {
                throw new ArgumentException("Param count does not match. " + paramList.Count() + " given, " + _paramCount + " expected");
            }

            DinPin = paramList[1];            
            ClsPin = paramList[2];
            ClkPin = paramList[3];
            byte brightness = 15;
            Byte.TryParse(paramList[4], out brightness);
            Brightness = brightness;
            NumModules = paramList[5];
            Name = paramList[6];

            return true;
        }
    }
}